using FluentValidation;
using Microsoft.AspNetCore.Http;
using RentalManager.Infrastructure.Commands.UserCommands;

namespace RentalManager.Infrastructure.Validators.UserValidators;

public class UserBaseValidator : AbstractValidator<UserBaseCommand>
{
    public UserBaseValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100)
            .Matches("^[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻĆ ']*$")
            .WithMessage("'Name' should only contain letters");

        RuleFor(x => x.Surname)
            .NotEmpty()
            .MaximumLength(100)
            .Matches("^[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻĆ ']*$")
            .WithMessage("'Surname' should only contain letters");

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