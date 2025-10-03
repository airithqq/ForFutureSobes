using System.Reflection.Metadata.Ecma335;
using ForFutureSobes.Data;
using ForFutureSobes.Domain;
using ForFutureSobes.DTOs;
using ForFutureSobes.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        /// Create new unic theme
        /// </summary>
        [HttpPost("CreateNewTheme")]
        public async Task<IActionResult> CreateTheme([FromBody] CreateThemeDTO dto)
        {

            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Theme name is required");
            var theme = new Theme { Name = dto.Name };

            if (await _themeService.CreateThemeAsync(dto))
            {
                return Conflict("Theme already exists");
            }else
                return CreatedAtAction(nameof(_themeService.GetAllThemesAsync), new { id = theme.Id }, theme);
        }

        /// <summary>
        /// Delete theme
        /// </summary>
        [HttpDelete("Delete")]
        public async Task DeleteAsync(string themeName) => await _themeService.DeleteThemeAsync(themeName);
        
    }
}
