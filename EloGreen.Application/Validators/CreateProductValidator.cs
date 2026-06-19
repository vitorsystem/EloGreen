using EloGreen.Application.ViewModels.Request;
using FluentValidation;

namespace EloGreen.Application.Validators;

public class CreateProductValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome do produto é obrigatório.")
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(500);

        RuleFor(x => x.BaseCarbonFootprint)
            .GreaterThanOrEqualTo(0).WithMessage("A pegada de carbono não pode ser negativa.");

        RuleFor(x => x.SupplierId)
            .NotEmpty().WithMessage("O produto precisa estar vinculado a um fornecedor válido.");
    }
}