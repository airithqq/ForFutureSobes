using ForFutureSobes.Domain;
using ForFutureSobes.DTOs;
using ForFutureSobes.Interfaces;
using ForFutureSobes.Repository;
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

      
        public async Task<bool> CreateThemeAsync([FromBody] CreateThemeDTO dto)
        {
          
            return await _themeRepository.CreateThemeAsync(dto);
        }

        public async Task DeleteThemeAsync(string themeName) => await _themeRepository.DeleteThemeAsync(themeName);
    }
}
