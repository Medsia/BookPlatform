using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookPlatform.MQ.Interfaces
{
    public interface IMessageBrokerService
    {
        void SendMessage(string message, string routingKey, string queueName);
        string ReceiveMessage(string routingKey, string queueName);
    }
}
