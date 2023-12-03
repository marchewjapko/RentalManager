using FluentValidation;
using Microsoft.AspNetCore.Http;
using RentalManager.Infrastructure.Commands.EmployeeCommands;

namespace RentalManager.Infrastructure.Services.Validators;

public class EmployeeValidator : AbstractValidator<EmployeeBaseCommand>
{
    public EmployeeValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100)
            .Matches(@"^[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻ]*$")
            .WithMessage("'Name' should only contain letters");

        RuleFor(x => x.Surname)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100)
            .Matches(@"^[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻ]*$")
            .WithMessage("'Surname' should only contain letters");

        RuleFor(x => x.Gender)
            .NotNull();

        When(x => x.Image is not null, () => {
            RuleFor(x => x.Image)
                .Must(x => x.Length <= 1024 * 1024)
                .WithMessage("File too large, maximum file size is 1 MB");
            RuleFor(x => x.Image)
                .Must(ValidateExtension)
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