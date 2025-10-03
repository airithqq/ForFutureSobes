using System.Threading.Tasks;
using ForFutureSobes.Data;
using ForFutureSobes.Domain;
using ForFutureSobes.DTOs;
using ForFutureSobes.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ForFutureSobes.Data;

namespace ForFutureSobes.Repository
{
    public class ThemeRepository : IThemeRepository
    {
        private readonly ForFutureSobesDbContext context;


        public ThemeRepository(ForFutureSobesDbContext context)
        {
              this.context = context;
        }

        public  Task<Theme?> GetByName(string themeName) =>  context.Themes.FirstOrDefaultAsync(x => x.Name == themeName);


        public async Task<List<Theme?>> GetAllThemes() => await context.Themes.ToListAsync();

        
        public async Task<bool> CreateTheme([FromBody] CreateThemeDTO dto)
        {
            var exists =  await context.Themes.AnyAsync(x => x.Name == dto.Name);
            var theme = new Theme { Name = dto.Name };
            context.Themes.Add(theme);
            await context.SaveChangesAsync();
            return exists;

        }

        public async Task DeleteTheme(string themeName)
        {
            var themeToDelete = context.Themes
                .FirstOrDefault(x => x.Name == themeName);
            if(themeToDelete != null)
            {
                context.Themes.Remove(themeToDelete);
                await context.SaveChangesAsync();
            }
            
        }
    }
}
