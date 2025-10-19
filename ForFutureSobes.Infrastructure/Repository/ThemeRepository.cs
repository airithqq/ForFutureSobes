using ForFutureSobes.Infrastructure.Data;
using ForFutureSobes.Model.Domain;
using ForFutureSobes.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ForFutureSobes.Infrastructure.Repository
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
     

        public async Task<Theme> CreateThemeAsync([FromBody] string themeName )
        {
            var exists =  await _context.Themes.AnyAsync(x => x.Name == themeName);
            var theme = new Theme { Name = themeName};
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
