namespace RentalManager.Infrastructure.Validators.UserValidators;

public class UpdateUserValidator : UserBaseValidator
{
    public UpdateUserValidator()
    {
        RuleFor(x => x)
            .SetValidator(new UserBaseValidator());
    }
}