using BookPlatform.MQ.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookPlatform.MQ.Services
{
    public class RabbitMQService : IMessageBrokerService
    {
        private readonly IConfiguration _configuration;
        private readonly ConnectionFactory _factory;

        public RabbitMQService(IConfiguration configuration)
        {
            _configuration = configuration;
            _factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

        }

        public void SendMessage(string message, string routingKey, string queueName)
        {
            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: queueName, type: ExchangeType.Direct);

                    channel.QueueBind(queue: queueName,
                                         exchange: queueName,
                                         routingKey: routingKey);

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: queueName,
                                         routingKey: routingKey,
                                         basicProperties: null,
                                         body: body);
                }
            }
        }

        public string ReceiveMessage(string routingKey, string queueName)
        {
            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: queueName, type: ExchangeType.Direct);

                    channel.QueueBind(queue: queueName,
                                         exchange: queueName,
                                         routingKey: routingKey);


                    string message = null;

                    var basicGetResult = channel.BasicGet(queueName, false);
                    var timeoutSeconds = 60;
                    var timer = 0;
                    while (true)
                    {
                        basicGetResult = channel.BasicGet(queueName, true);

                        if (basicGetResult != null)
                        {
                            message = Encoding.UTF8.GetString(basicGetResult.Body.ToArray());
                            return message;
                        }
                        else
                        {
                            Thread.Sleep(1000);
                            timer++;
                            if (timer > timeoutSeconds)
                            {
                                throw new TimeoutException();
                            }

                        }
                    }

                }
            }
        }
    }
}
