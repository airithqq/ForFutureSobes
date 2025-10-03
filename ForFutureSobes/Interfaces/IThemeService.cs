using ForFutureSobes.Domain;
using ForFutureSobes.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ForFutureSobes.Interfaces
{
    public interface IThemeService
    {
        public Task<List<Theme>> GetAllThemesAsync();
        public Task<bool> CreateThemeAsync([FromBody] CreateThemeDTO dto);
        Task DeleteThemeAsync(string themeName);
    }
}
