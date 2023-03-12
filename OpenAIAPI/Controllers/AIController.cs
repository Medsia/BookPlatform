using BookPlatform.AIAPI.Interfaces;
using BookPlatform.AIAPI.Models;
using BookPlatform.MQ.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookPlatform.AIAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AIController : ControllerBase
    {
        private readonly IAIService _AIService;
        private readonly IMessageBrokerService _messageBrokerService;
        private readonly IConfiguration _configuration;

        public AIController(IAIService adProduct, IMessageBrokerService messageBrokerService,
            IConfiguration configuration)
        {
            _AIService = adProduct;
            _messageBrokerService = messageBrokerService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult<AIResponseModel>> GenerateAIContent(UserRequestModel generateRequestModel)
        {
            try
            {
                var response = await _AIService.GenerateAIContent(generateRequestModel);

                return response;
            }
            catch (System.Exception ex)
            {

                var response = new AIResponseModel();
                response.Success = false;
                response.Content.Add(ex.Message);

                return response;
            }

        }
    }
}
