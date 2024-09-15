using FluentValidation;
using RentalManager.Infrastructure.Models.Commands.EquipmentCommands;

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
    }
}