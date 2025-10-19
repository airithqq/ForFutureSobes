using ForFutureSobes.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using ForFutureSobes.Application.Interfaces;
using ForFutureSobes.Infrastructure.Interfaces;
using ForFutureSobes.Model.Domain;

namespace ForFutureSobes.Application.Services
{
    public class ThemeService : IThemeService
    {
        private readonly IThemeRepository _themeRepository;

        public ThemeService(IThemeRepository themeRepository)
        {
                _themeRepository = themeRepository;
        }
        public async Task<List<Theme>> GetAllThemesAsync() => await _themeRepository.GetAllThemesAsync();

        public async Task<Theme> CreateThemeAsync([FromBody] string themeName)
        {
          
            return await _themeRepository.CreateThemeAsync(themeName);
        }

        public async Task<Theme> GetByIdAsync(int id) => await _themeRepository.GetByIdAsync(id);

        public async Task DeleteThemeAsync(string themeName) => await _themeRepository.DeleteThemeAsync(themeName);
    
    }
}
