using FluentValidation;
using RentalManager.Infrastructure.Commands.EmployeeCommands;

namespace RentalManager.Infrastructure.Validators.EmployeeValidators;

public class CreateEmployeeValidator : AbstractValidator<CreateEmployee>
{
    public CreateEmployeeValidator()
    {
        RuleFor(x => x)
            .SetValidator(new EmployeeBaseValidator());
        
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(100)
            .Must(x => x.Any(char.IsNumber))
            .WithMessage("Password must contain a number");

        RuleFor(x => x.UserName)
            .NotEmpty()
            .MaximumLength(100)
            .Matches(@"^[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻĆ 0-9\/.\/+\-\/%]*$");
    }
}