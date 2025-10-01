using ForFutureSobes.Data;
using ForFutureSobes.Domain;
using ForFutureSobes.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForFutureSobes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThemesController : ControllerBase
    {

            private readonly ForFutureSobesDbContext _context;

            public ThemesController(ForFutureSobesDbContext context)
            {
                _context = context;
            }
          
            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var themes = await _context.Themes.ToListAsync();
                return Ok(themes);
            }

            [HttpPost]
            public async Task<IActionResult> Create([FromBody] CreateThemeDTO dto)
            {
                if (string.IsNullOrWhiteSpace(dto.Name))
                    return BadRequest("Theme name is required");

                var exists = await _context.Themes.AnyAsync(t => t.Name == dto.Name);
                if (exists)
                    return Conflict("Theme already exists");

                var theme = new Theme { Name = dto.Name };
                _context.Themes.Add(theme);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAll), new { id = theme.Id }, theme);
            }
        
    }
}
