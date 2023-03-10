using BookPlatform.AIAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookPlatform.AIAPI.Interfaces
{
    public interface IAIAPIService
    {
        Task<List<string>> GenerateContent(AIGenerateRequestModel generateRequestModel);
    }
}
