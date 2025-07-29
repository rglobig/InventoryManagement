using FluentValidation;

namespace InventoryManagement.Application.DataTransferObjects.Validation;

internal sealed class UpdateInventoryItemDtoValidator : AbstractValidator<UpdateInventoryItemDto>
{
    public UpdateInventoryItemDtoValidator()
    {
        const string onlyLettersAndDigits = @"^[a-zA-Z0-9\s]+$";
        RuleFor(x => x.Name)
            .MinimumLength(3)
            .Matches(onlyLettersAndDigits)
            .When(x => !string.IsNullOrEmpty(x.Name))
            .WithMessage("Name must be at least 3 characters long.");

        RuleFor(x => x.Description)
            .Matches(onlyLettersAndDigits)
            .When(x => !string.IsNullOrEmpty(x.Description))
            .WithMessage("Description must contain only letters and digits.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .When(x => x.Quantity.HasValue)
            .WithMessage("Quantity must be greater than 0.");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .When(x => x.Price.HasValue)
            .WithMessage("Price must be greater than 0.");
    }
}
