using BookPlatform.AI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookPlatform.AI.Interfaces
{
    public interface IAIService
    {
        Task<AIResponseModel> GenerateAIContent(UserRequestModel generateRequestModel);
    }
}
