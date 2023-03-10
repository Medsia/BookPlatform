using BookPlatform.MQ.Interfaces;
using BookPlatform.MQ.Services;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace BookPlatform.AIAPI.Services
{
    public class RabbitMQListener : IDisposable
    {
        private readonly IMessageBrokerService _rabbitMQService;
        private readonly IConfiguration _configuration;
        private readonly string _routingKey;
        private readonly string _queueName;
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQListener(IMessageBrokerService rabbitMQService, string routingKey,
            string queueName, IConfiguration configuration)
        {
            _configuration = configuration;
            _factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _queueName = queueName;
            _rabbitMQService = rabbitMQService;
            _routingKey = routingKey;
            _queueName = queueName;
        }

        public void StartListening()
        {
            Console.WriteLine($"Start listening to queue {_queueName}");
            _channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false);
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                ProcessMessage(message);
                Console.WriteLine($"Received message with routing key {_routingKey}: {message}");
            };

            _channel.BasicConsume(queue: _queueName,
                                                 autoAck: true,
                                                 consumer: consumer);
        }

        public virtual void ProcessMessage(string message)
        {

        }
        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
