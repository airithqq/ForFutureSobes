using ForFutureSobes.Domain;
using ForFutureSobes.DTOs;
using Microsoft.AspNetCore.Mvc;
namespace ForFutureSobes.Interfaces
{
    public interface IThemeRepository
    {
        Task<Theme?> GetByNameAsync(string themeName);
        Task<List<Theme?>> GetAllThemesAsync();
        Task<bool> CreateThemeAsync([FromBody] CreateThemeDTO dto);
        Task DeleteThemeAsync(string themeName);
    }
}
