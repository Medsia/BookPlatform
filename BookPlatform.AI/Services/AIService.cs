using BookPlatform.AI.Interfaces;
using BookPlatform.AI.Models;

namespace BookPlatform.AI.Services
{
    public class AIService : IAIService
    {
        private readonly IAIAPIService _AIAPIService;

        public AIService(IAIAPIService AIAPIService)
        {
            _AIAPIService = AIAPIService;

        }

        public async Task<AIResponseModel> GenerateAIContent(UserRequestModel generateRequestModel)
        {
            if (string.IsNullOrEmpty(generateRequestModel.Message))
            {
                return new AIResponseModel
                {
                    Success = false,
                    Content = null
                };
            }
            var userMessage = new AIGenerateRequestModel
            {
                Prompt = generateRequestModel.Message
            };
            var generateAD = await _AIAPIService.GenerateContent(userMessage);

            if (generateAD.Count == 0)
            {
                return new AIResponseModel
                {
                    Success = false,
                    Content = null
                };
            }

            return new AIResponseModel
            {
                Success = true,
                Content = generateAD
            };
        }
    }
}
