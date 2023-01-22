using System.Globalization;
using System.Text;
using System.Threading.Channels;
using Coinbase.Api.EventProcessing;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Coinbase.Api.AsyncDataSubscriber
{
    public class RmqMessageBusConsumer : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IEventProcessor _eventProcessor;
        private IConnection? _connection;
        protected IModel? Channel { get; private set; }
        private string? _queueName;

        private readonly ILogger<RmqMessageBusConsumer> _logger;

        public RmqMessageBusConsumer(
           IConfiguration configuration,
           IEventProcessor eventProcessor,
           ILogger<RmqMessageBusConsumer> logger)
        {
            _configuration = configuration;
            _eventProcessor = eventProcessor;
            _logger = logger;

            InitializeRabbitMq();
        }

        public void InitializeRabbitMq()
        {
            try
            {
                ConnectionFactory connectionFactory = new()
                {
                    HostName = _configuration["RabbitMQHost"],
                    Port = int.Parse(_configuration["RabbitMQPort"], CultureInfo.CurrentUICulture.NumberFormat)
                };

                if (_connection == null || _connection.IsOpen)
                {
                    _connection = connectionFactory.CreateConnection();
                }

                if (Channel == null || Channel.IsOpen)
                {
                    Channel = _connection.CreateModel();

                    Channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
                    _queueName = Channel.QueueDeclare().QueueName;
                    Channel.QueueBind(queue: _queueName,
                        exchange: "trigger",
                        routingKey: "");

                    _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
                }
            }                
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Cannot connect to RabbitMQ channel");
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            EventingBasicConsumer consumer = new(Channel);

            consumer.Received += async (moduleHandle, ea) =>
            {
                ReadOnlyMemory<byte> body = ea.Body;
                string notificationMessage = Encoding.UTF8.GetString(body.ToArray());

                await _eventProcessor.ProcessEventAsync(notificationMessage);
            };

            try
            {
                // Continue consuming on the same queue
                Channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Cannot Execute basic consume RabbitMQ connection");
            }

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            try
            {
                Channel?.Close();
                Channel?.Dispose();
                Channel = null;

                _connection?.Close();
                _connection?.Dispose();
                _connection = null;

                GC.SuppressFinalize(this);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Cannot dispose RabbitMQ channel or connection");
            }

            base.Dispose();
        }

        private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e)
        { 
            _logger.LogCritical(e.ReplyText, "RabbitMQ connection shutdown");
        }
    }
}
