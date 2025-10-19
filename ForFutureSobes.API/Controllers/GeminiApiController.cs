using ForFutureSobes.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ForFutureSobes.Helper;

namespace ForFutureSobes.Controllers
{
    [Route("api/v1/gemini[controller]")]
    [ApiController]
    public class GeminiApiController : ControllerBase
    {
        private readonly IGeminiService _geminiService;
        public GeminiApiController(IGeminiService geminiService)
        {
            _geminiService = geminiService;
        }
        /// <summary>
        /// Ask Gemini about task that not completed 
        /// </summary>
        [HttpPost("ask")]
        public async Task<IActionResult> Ask([FromQuery] string variant, int taskId)
        {
            string prompt = await _geminiService.GetTaskSummariesAsync(variant,taskId);
            var reply = await _geminiService.SendAsync(prompt);
            return Content(reply, "text/html");
        }

    }
}
