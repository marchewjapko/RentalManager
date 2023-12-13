namespace RentalManager.Infrastructure.Validators.EmployeeValidators;

public class UpdateEmployeeValidator : EmployeeBaseValidator
{
    public UpdateEmployeeValidator()
    {
        RuleFor(x => x)
            .SetValidator(new EmployeeBaseValidator());
    }
}