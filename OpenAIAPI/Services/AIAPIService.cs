using BookPlatform.AIAPI.Interfaces;
using BookPlatform.AIAPI.Models;
using Microsoft.Extensions.Configuration;
using OpenAI_API;
using System.Text;

namespace BookPlatform.AIAPI.Services
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
            var apiKey = Encoding.UTF8.GetString(Convert.FromBase64String(_configuration.GetSection("Appsettings:GChatAPIKEY").Value));
            var apiModel = _configuration.GetSection("Appsettings:Model").Value;
            List<string> rq = new List<string>();
            string rs = "";
            OpenAIAPI api = new OpenAIAPI(new APIAuthentication(apiKey));
            var completionRequest = new OpenAI_API.Completions.CompletionRequest()
            {
                Prompt = generateRequestModel.Prompt,
                Model = apiModel,
                Temperature = 0.5,
                MaxTokens = 300,
                TopP = 1.0,
                FrequencyPenalty = 0.0,
                PresencePenalty = 0.0,
                NumChoicesPerPrompt = 1

            };
            var result = await api.Completions.CreateCompletionsAsync(completionRequest, 1);
            foreach (var choice in result.Completions)
            {
                rs = choice.Text;
                rq.Add(choice.Text);
            }
            return rq;
        }

    }
}