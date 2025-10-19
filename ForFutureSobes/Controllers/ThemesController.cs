
using ForFutureSobes.Application.Interfaces;
using ForFutureSobes.Model.Domain;
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

        /// <summary>
        /// Get theme by id
        /// </summary>
        [HttpGet("GetThemeById")]
        public async Task<IActionResult> GetById(int id)
        {
            var theme = await _themeService.GetByIdAsync(id);
            if (theme == null)
                return NotFound($"Theme with id {id} not found.");

            return Ok(theme);
        }

        /// <summary>
        /// Create new unique theme
        /// </summary>
        [HttpPost("CreateNewTheme")]
        public async Task<IActionResult> CreateTheme(string themeName)
        {
            if (themeName == null)
                return BadRequest("Request body cannot be empty");
            if (string.IsNullOrWhiteSpace(themeName))
                return BadRequest("Theme name is required");
            var theme = await _themeService.CreateThemeAsync(themeName);
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