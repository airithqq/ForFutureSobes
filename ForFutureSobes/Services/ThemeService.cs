using ForFutureSobes.Domain;
using ForFutureSobes.DTOs;
using ForFutureSobes.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ForFutureSobes.Services
{
    public class ThemeService : IThemeService
    {
        private readonly IThemeRepository _themeRepository;

        public ThemeService(IThemeRepository themeRepository)
        {
                _themeRepository = themeRepository;
        }
        public async Task<List<Theme>> GetAllThemesAsync() => await _themeRepository.GetAllThemesAsync();

        public async Task<Theme> CreateThemeAsync([FromBody] CreateThemeDTO dto)
        {
          
            return await _themeRepository.CreateThemeAsync(dto);
        }

        public async Task<Theme> GetByIdAsync(int id) => await _themeRepository.GetByIdAsync(id);

        public async Task DeleteThemeAsync(string themeName) => await _themeRepository.DeleteThemeAsync(themeName);
    
    }
}
