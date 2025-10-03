using ForFutureSobes.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ForFutureSobes.DTOs;
namespace ForFutureSobes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeminiController : ControllerBase
    {
        private readonly IGeminiService _geminiService;

        public GeminiController(IGeminiService geminiService)
        {
            _geminiService = geminiService;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateText([FromBody] GeminiDTOs.GenerateTextRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _geminiService.GenerateTextAsync(request.Prompt, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Unexpected error: {ex.Message}");
            }
        }
    }
}
