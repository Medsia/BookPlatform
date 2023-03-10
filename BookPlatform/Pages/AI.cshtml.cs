using BookPlatform.AIAPI.Models;
using BookPlatform.MQ.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace BookPlatform.Pages
{
    [IgnoreAntiforgeryToken]
    public class AIModel : PageModel
    {
        private readonly IMessageBrokerService _messageBrokerService;
        private readonly IConfiguration _configuration;
        public string AIResponse { get; set; }

        public AIModel(IMessageBrokerService messageBrokerService,
            IConfiguration configuration)
        {
            _messageBrokerService = messageBrokerService;
            _configuration = configuration;
        }
        public void OnGet()
        {

        }

        public IActionResult OnPost([FromBody] UserRequestModel userRequestModel)
        {
            var response = SendRequest(userRequestModel);
            return new JsonResult(response);
        }
        public string SendRequest(UserRequestModel userRequestModel)
        {
            var routingKey = new Guid().ToString();

            var JSONRequest = JsonConvert.SerializeObject(userRequestModel);
            _messageBrokerService.SendMessage(JSONRequest, routingKey, _configuration.GetSection("MQsettings:IncomingAIQueueName").Value);

            var JSONResponse = _messageBrokerService.ReceiveMessage(routingKey, _configuration.GetSection("MQsettings:OutgoingAIQueueName").Value);

            return JSONResponse;
        }
    }
}

