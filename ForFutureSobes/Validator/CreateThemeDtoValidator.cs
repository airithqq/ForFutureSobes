using FluentValidation;
using ForFutureSobes.Data;
using ForFutureSobes.DTOs;
namespace ForFutureSobes.Validator
{
    public class CreateThemeDtoValidator : AbstractValidator<CreateThemeDTO>
    {
        private readonly ForFutureSobesDbContext _db;
        public CreateThemeDtoValidator(ForFutureSobesDbContext db)
        {
            _db = db;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Theme name is required")
                .Length(2, 50).WithMessage("Theme name must be between 2 and 50 characters")
                .Must(name => !_db.Themes.Any(t => t.Name == name))
                .WithMessage("Theme with this name already exists");
        }

    }
}
