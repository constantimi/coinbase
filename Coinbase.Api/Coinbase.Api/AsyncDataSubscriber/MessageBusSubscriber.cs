using System.Globalization;
using System.Text;
using Coinbase.Api.EventProcessing;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Coinbase.Api.AsyncDataSubscriber
{
    public class MessageBusSubscriber : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IEventProcessor _eventProcessor;
        private IConnection _connection;
        private IModel _channel;
        private string _queueName;

        public MessageBusSubscriber(
           IConfiguration configuration,
           IEventProcessor eventProcessor)
        {
            _configuration = configuration;
            _eventProcessor = eventProcessor;

            InitializeRabbitMQ();
        }

        private void InitializeRabbitMQ()
        {
            ConnectionFactory factory = new()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"], CultureInfo.CurrentUICulture.NumberFormat)
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
            _queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: _queueName,
                exchange: "trigger",
                routingKey: "");

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShitdown;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            EventingBasicConsumer consumer = new(_channel);

            consumer.Received += async (moduleHandle, ea) =>
            {
                ReadOnlyMemory<byte> body = ea.Body;
                string notificationMessage = Encoding.UTF8.GetString(body.ToArray());

                await _eventProcessor.ProcessEventAsync(notificationMessage);
            };

            // Continue consuming on the same queue
            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        private void RabbitMQ_ConnectionShitdown(object? sender, ShutdownEventArgs e)
        { }

        public override void Dispose()
        {
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
                GC.SuppressFinalize(this);
            }

            base.Dispose();
        }
    }
}
