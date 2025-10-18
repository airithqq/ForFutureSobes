using ForFutureSobes.Model.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ForFutureSobes.Infrastructure.Interfaces
{
    public interface IThemeRepository
    {

        Task<List<Theme?>> GetAllThemesAsync();
        Task<Theme> CreateThemeAsync([FromBody] string themeName);
        Task DeleteThemeAsync(string themeName);
        Task<Theme?> GetByIdAsync(int id);
    }
}
