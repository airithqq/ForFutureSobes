using ForFutureSobes.Model.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ForFutureSobes.Application.Interfaces
{
    public interface IThemeService
    {
        Task<List<Theme>> GetAllThemesAsync();
        Task<Theme> CreateThemeAsync([FromBody] string themeName);
        Task DeleteThemeAsync(string themeName);
        Task<Theme> GetByIdAsync(int id);
    }
}
