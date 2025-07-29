using FluentValidation;

namespace InventoryManagement.Application.DataTransferObjects.Validation;

internal sealed class CreateInventoryItemDtoValidator : AbstractValidator<CreateInventoryItemDto>
{
    public CreateInventoryItemDtoValidator()
    {
        const string onlyLettersAndDigits = @"^[a-zA-Z0-9\s]+$";
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3)
            .Matches(onlyLettersAndDigits)
            .WithMessage("Name is required. It must be at least 3 characters long.");

        RuleFor(x => x.Description)
            .Matches(onlyLettersAndDigits)
            .When(x => !string.IsNullOrEmpty(x.Description))
            .WithMessage("Description must contain only letters and digits.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");
    }
}
