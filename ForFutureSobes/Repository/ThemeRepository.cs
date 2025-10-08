using ForFutureSobes.Data;
using ForFutureSobes.Domain;
using ForFutureSobes.DTOs;
using ForFutureSobes.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForFutureSobes.Repository
{
    public class ThemeRepository : IThemeRepository
    {
        private readonly ForFutureSobesDbContext _context;

        public ThemeRepository(ForFutureSobesDbContext context)
        {
              _context = context;
        }

        public async Task<List<Theme?>> GetAllThemesAsync() => await _context.Themes.ToListAsync();

        public async Task<Theme?> GetByIdAsync(int id) => await _context.Themes
                                                            .AsNoTracking()
                                                            .FirstOrDefaultAsync(t => t.Id == id);
     

        public async Task<Theme> CreateThemeAsync([FromBody] CreateThemeDTO dto)
        {
            var exists =  await _context.Themes.AnyAsync(x => x.Name == dto.Name);
            var theme = new Theme { Name = dto.Name };
            _context.Themes.Add(theme);
            await _context.SaveChangesAsync();
            return theme;

        }

        public async Task DeleteThemeAsync(string themeName)
        {
            var themeToDelete = _context.Themes
                .FirstOrDefault(x => x.Name == themeName);
            if(themeToDelete != null)
            {
                _context.Themes.Remove(themeToDelete);
                await _context.SaveChangesAsync();
            }
            
        }
    }
}
