using ForFutureSobes.Domain;
using ForFutureSobes.DTOs;
using ForFutureSobes.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ForFutureSobes.Controllers
{
    [ApiController]
    [Route("api/v1/theme")]
    public class ThemesController : ControllerBase
    {

        private readonly IThemeService _themeService;
        public ThemesController(IThemeService themeService)
        {
            _themeService = themeService;
        }

        /// <summary>
        /// Get all existing themes
        /// </summary>
        [HttpGet("GetAllThemes")]
        public async Task<List<Theme>> GetAllThemes() => await _themeService.GetAllThemesAsync();


        [HttpGet("GetThemeById")]
        public async Task<IActionResult> GetById(int id)
        {
            var theme = await _themeService.GetByIdAsync(id);
            if (theme == null)
                return NotFound($"Theme with id {id} not found.");

            return Ok(theme);
        }
   
        /// <summary>
        /// Create new unic theme
        /// </summary>
        [HttpPost("CreateNewTheme")]
        public async Task<IActionResult> CreateTheme([FromBody] CreateThemeDTO dto)
        {
            if (dto == null)
                return BadRequest("Request body cannot be empty");

            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Theme name is required");

            var theme = await _themeService.CreateThemeAsync(dto);

            if (theme == null)
                return StatusCode(500, "Failed to create theme.");

            return CreatedAtAction(nameof(GetById), new { id = theme.Id }, theme);
        }

            /// <summary>
            /// Delete theme
            /// </summary>
            [HttpDelete("Delete")]
            public async Task DeleteAsync(string themeName) => await _themeService.DeleteThemeAsync(themeName);

        
    }
}