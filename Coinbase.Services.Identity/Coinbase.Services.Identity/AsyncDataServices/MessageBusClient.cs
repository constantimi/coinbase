using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using Coinbase.Services.Identity.Models;
using System.Globalization;
using Coinbase.Services.Identity.Helpers;

namespace Coinbase.Services.Identity.Services.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionFactory factory = new()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"], CultureInfo.CurrentUICulture.NumberFormat)
            };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

            }
            catch (AppException ex)
            {
                throw new AppException($"Could not connect to the Message Bus: {ex.Message}");
            }
        }

        public void PublishNewOwner(PublisherRequest ownerPublishedRequest)
        {
            string message = JsonSerializer.Serialize(ownerPublishedRequest);

            if (_connection.IsOpen)
            {
                SendMessage(message);
            }
        }

        private void SendMessage(string message)
        {
            byte[] body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "trigger",
                            routingKey: "",
                            basicProperties: null,
                            body: body);
        }

        public void Dispose()
        {
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        { }
    }
}
