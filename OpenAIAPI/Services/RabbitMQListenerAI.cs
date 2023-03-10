using BookPlatform.AIAPI.Interfaces;
using BookPlatform.AIAPI.Models;
using BookPlatform.MQ.Interfaces;
using Newtonsoft.Json;

namespace BookPlatform.AIAPI.Services
{
    public class RabbitMQListenerAI : RabbitMQListener
    {
        private readonly IAIService _AIService;
        private readonly IMessageBrokerService _messageBrokerService;
        private readonly IConfiguration _configuration;
        private readonly string _routingKey;
        private readonly string _queueName;
        public RabbitMQListenerAI(IMessageBrokerService rabbitMQService, string routingKey,
            string queueName, IConfiguration configuration, IAIService aIService) : base(rabbitMQService, routingKey, queueName, configuration)
        {
            _AIService = aIService;
            _messageBrokerService = rabbitMQService;
            _routingKey = routingKey;
            _queueName = queueName;
            _configuration = configuration;
        }

        public async override void ProcessMessage(string message)
        {
            try
            {

                var JSONMessage = JsonConvert.DeserializeObject<UserRequestModel>(message);

                var response = await _AIService.GenerateAIContent(JSONMessage);

                var JSONResponse = JsonConvert.SerializeObject(response);
                _messageBrokerService.SendMessage(JSONResponse, _routingKey, _configuration.GetSection("MQsettings:OutgoingAIQueueName").Value);

            }
            catch (System.Exception ex)
            {

            }
        }
    }
}
