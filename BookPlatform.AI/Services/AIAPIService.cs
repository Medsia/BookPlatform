using BookPlatform.AI.Interfaces;
using BookPlatform.AI.Models;
using Microsoft.Extensions.Configuration;
using OpenAI_API;

namespace BookPlatform.AI.Services
{
    public class AIAPIService : IAIAPIService
    {
        private readonly IConfiguration _configuration;

        public AIAPIService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<string>> GenerateContent(AIGenerateRequestModel generateRequestModel)
        {
            var apiKey = _configuration.GetSection("Appsettings:GChatAPIKEY").Value;
            var apiModel = _configuration.GetSection("Appsettings:Model").Value;
            List<string> rq = new List<string>();
            string rs = "";
            OpenAIAPI api = new OpenAIAPI(new APIAuthentication(apiKey));
            var completionRequest = new OpenAI_API.Completions.CompletionRequest()
            {
                Prompt = generateRequestModel.Prompt,
                Model = apiModel,
                Temperature = 0.5,
                MaxTokens = 100,
                TopP = 1.0,
                FrequencyPenalty = 0.0,
                PresencePenalty = 0.0,

            };
            var result = await api.Completions.CreateCompletionsAsync(completionRequest);
            foreach (var choice in result.Completions)
            {
                rs = choice.Text;
                rq.Add(choice.Text);
            }
            return rq;
        }

    }
}