using ForFutureSobes.Infrastructure.Data;
using ForFutureSobes.Application.DTOs;
using FluentValidation;

namespace ForFutureSobes.Application.Validator
{
    public class CreateTaskDtoValidator : AbstractValidator<CreateTaskDTO>
    {
        private readonly ForFutureSobesDbContext _db;
        private readonly HashSet<string> _themeNames;
        private static readonly string[] AllowedPriorities = { "High", "Middle", "Low" };

        public CreateTaskDtoValidator(ForFutureSobesDbContext db)
        {
            _db = db;

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .Length(5, 100).WithMessage("Title must be between 3 and 50 characters");
            RuleFor(x => x.Description)
                .Length(5, 100).WithMessage("Description must be between 3 and 50 characters");
            _themeNames = _db.Themes
          .Select(t => t.Name)
          .ToHashSet(StringComparer.OrdinalIgnoreCase);
            RuleFor(x => x.ThemeName)
                .NotEmpty().WithMessage("Theme is required")
                .Must(theme => _themeNames.Contains(theme))
                .WithMessage("Theme is not found");
            RuleFor(x => x.Priority)
                .NotEmpty().WithMessage("Priority is required")
                .Must(p => AllowedPriorities.Contains(p))
                .WithMessage("Priority must be one of: High, Middle, Low");

        }

    }

}


