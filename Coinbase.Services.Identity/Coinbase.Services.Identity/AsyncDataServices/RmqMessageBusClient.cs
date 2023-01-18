using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using Coinbase.Services.Identity.Models;
using System.Globalization;

namespace Coinbase.Services.Identity.Services.AsyncDataServices
{
    public class RmqMessageBusClient : IRmqMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private IConnection _connection;
        protected IModel Channel { get; private set; }

        private readonly ILogger<RmqMessageBusClient> _logger;


        public RmqMessageBusClient(IConfiguration configuration, ILogger<RmqMessageBusClient> logger)
        {
            _configuration = configuration;
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

                if (_connection == null || _connection.IsOpen == false)
                {
                    _connection = connectionFactory.CreateConnection();
                }

                if (Channel == null || Channel.IsOpen == false)
                {
                    Channel = _connection.CreateModel();

                    Channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

                    _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Cannot connect to RabbitMQ channel");
            }
        }

        public void PublishNewOwner(RmqProducerRequest rmqpProducerRequest)
        {
            string message = JsonSerializer.Serialize(rmqpProducerRequest);

            if (_connection.IsOpen)
            {
                SendMessage(message);
            }
        }

        private void SendMessage(string message)
        {
            byte[] body = Encoding.UTF8.GetBytes(message);

            Channel.BasicPublish(exchange: "trigger",
                            routingKey: "",
                            basicProperties: null,
                            body: body);
        }

        public void Dispose()
        {
            try
            {
                Channel?.Close();
                Channel?.Dispose();
                Channel = null;

                _connection?.Close();
                _connection?.Dispose();
                _connection = null;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Cannot dispose RabbitMQ channel or connection");
            }
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        { }
    }
}
