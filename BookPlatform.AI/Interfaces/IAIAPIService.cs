using BookPlatform.AI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookPlatform.AI.Interfaces
{
    public interface IAIAPIService
    {
        Task<List<string>> GenerateContent(AIGenerateRequestModel generateRequestModel);
    }
}
