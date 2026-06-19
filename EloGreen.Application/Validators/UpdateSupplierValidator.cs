using EloGreen.Application.ViewModels.Request;
using FluentValidation;

namespace EloGreen.Application.Validators;

public class UpdateSupplierValidator : AbstractValidator<UpdateSupplierRequest>
{
    public UpdateSupplierValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(150);
        RuleFor(x => x.Document)
            .NotEmpty()
            .Length(14)
            .Matches("^[a-zA-Z0-9]{12}[0-9]{2}$").WithMessage("O CNPJ deve conter 12 caracteres alfanuméricos seguidos por 2 dígitos verificadores numéricos.");
    }
}