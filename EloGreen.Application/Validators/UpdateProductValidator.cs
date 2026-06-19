using EloGreen.Application.ViewModels.Request;
using FluentValidation;

namespace EloGreen.Application.Validators;

public class UpdateProductValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome do produto é obrigatório.")
            .MaximumLength(100).WithMessage("O nome não pode exceder 100 caracteres.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("A descrição não pode exceder 500 caracteres.");

        RuleFor(x => x.BaseCarbonFootprint)
            .GreaterThanOrEqualTo(0).WithMessage("A pegada de carbono base não pode ser negativa.");

        RuleFor(x => x.SupplierId)
            .NotEmpty().WithMessage("O produto precisa estar vinculado a um fornecedor válido.");
    }
}