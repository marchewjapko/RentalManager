using FluentValidation;
using Microsoft.AspNetCore.Http;
using RentalManager.Infrastructure.Commands.EquipmentCommands;

namespace RentalManager.Infrastructure.Validators.EquipmentValidators;

public class EquipmentBaseValidator : AbstractValidator<EquipmentBaseCommand>
{
    public EquipmentBaseValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Matches(@"^[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻĆ 0-9\/.\/+\-\/%]*$")
            .WithMessage(
                "'Name' should only contain letters, numbers, dots, slashes, minuses, pluses and percents ")
            .MaximumLength(100);

        RuleFor(x => x.Price)
            .NotEmpty()
            .GreaterThan(0);

        When(x => x.Image is not null, () => {
            RuleFor(x => x.Image)
                .Must(x => x!.Length <= 1024 * 1024)
                .WithMessage("File too large, maximum file size is 1 MB");
            RuleFor(x => x.Image)
                .Must(ValidateExtension!)
                .WithMessage(
                    "Unacceptable extension, allowed extensions: \n.png\n.jpg\n.jpeg\n.svg");
        });
    }

    private static bool ValidateExtension(IFormFile file)
    {
        string[] acceptableExtensions = { ".png", ".jpg", ".jpeg", ".svg" };
        var extension = Path.GetExtension(file.FileName);

        return acceptableExtensions.Contains(extension);
    }
}