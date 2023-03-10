using BookPlatform.AI.Interfaces;
using BookPlatform.AI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookPlatform.AI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AIController : ControllerBase
    {

        private readonly IAIService _adProduct;
        public AIController(IAIService adProduct)
        {
            _adProduct = adProduct;
        }

        //[HttpPost]
        public async Task<ActionResult<AIResponseModel>> GenerateAIContent(UserRequestModel generateRequestModel)
        {
            try
            {
                var response = await _adProduct.GenerateAIContent(generateRequestModel);

                return response;
            }
            catch (System.Exception ex)
            {

                return null;
            }

        }
    }
}
