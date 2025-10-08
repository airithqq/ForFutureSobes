using ForFutureSobes.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ForFutureSobes.Controllers
{
    [Route("api/v1/gemini[controller]")]
    [ApiController]
    public class GeminiApiController : ControllerBase
    {
        private readonly IGeminiService _gemini;
        public GeminiApiController(IGeminiService gemini)
        {
            _gemini = gemini;
        }
        /// <summary>
        /// Ask Gemini about task that not completed 
        /// </summary>
        [HttpPost("ask")]
        public async Task<IActionResult> Ask([FromBody] string request)
        {
            if (string.IsNullOrWhiteSpace(request))
                return BadRequest("Prompt is required");

            var reply = await _gemini.SendAsync(request);
            return Ok(new { reply });
        }
    }
}
