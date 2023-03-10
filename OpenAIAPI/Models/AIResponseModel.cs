using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookPlatform.AIAPI.Models
{
    public class AIResponseModel
    {
        public List<string> Content { get; set; }
        public bool Success { get; set; }
    }
}
