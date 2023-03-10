using BookPlatform.AIAPI.Interfaces;
using BookPlatform.AIAPI.Models;

namespace BookPlatform.AIAPI.Services
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
