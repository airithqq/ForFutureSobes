using ForFutureSobes.Domain;
using ForFutureSobes.DTOs;
using Microsoft.AspNetCore.Mvc;
namespace ForFutureSobes.Interfaces
{
    public interface IThemeRepository
    {

        Task<List<Theme?>> GetAllThemesAsync();
        Task<Theme> CreateThemeAsync([FromBody] CreateThemeDTO dto);
        Task DeleteThemeAsync(string themeName);
        Task<Theme?> GetByIdAsync(int id);
    }
}
