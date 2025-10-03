using ForFutureSobes.Domain;
using ForFutureSobes.DTOs;
using Microsoft.AspNetCore.Mvc;
namespace ForFutureSobes.Interfaces
{
    public interface IThemeRepository
    {
        Task<Theme?> GetByName(string themeName);
        Task<List<Theme?>> GetAllThemes();
        Task<bool> CreateTheme([FromBody] CreateThemeDTO dto);
        Task DeleteTheme(string themeName);
    }
}
