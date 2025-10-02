using ForFutureSobes.Domain;
using ForFutureSobes.DTOs;
using ForFutureSobes.Interfaces;
using ForFutureSobes.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ForFutureSobes.Services
{
    public class ThemeService : IThemeService
    {
        private readonly IThemeRepository themeRepository;

        public ThemeService(IThemeRepository themeRepository)
        {
                this.themeRepository = themeRepository;
        }



        public async Task<List<Theme>> GetAllThemesAsync() => await themeRepository.GetAllThemesAsync();

      
        public async Task<bool> CreateThemeAsync([FromBody] CreateThemeDTO dto)
        {
          
            return await themeRepository.CreateThemeAsync(dto);
        }

        public async Task DeleteThemeAsync(string themeName) => await themeRepository.DeleteThemeAsync(themeName);
    }
}
