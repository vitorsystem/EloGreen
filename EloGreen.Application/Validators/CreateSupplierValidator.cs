using EloGreen.Application.ViewModels.Request;
using FluentValidation;

namespace EloGreen.Application.Validators;

public class CreateSupplierValidator : AbstractValidator<CreateSupplierRequest>
{
    public CreateSupplierValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome do fornecedor é obrigatório.")
            .MaximumLength(150).WithMessage("O nome não pode exceder 150 caracteres.");

        RuleFor(x => x.Document)
            .NotEmpty().WithMessage("O documento (CNPJ) é obrigatório.")
            .Length(14).WithMessage("O CNPJ deve conter exatamente 14 caracteres (apenas números).")
            .Matches("^[a-zA-Z0-9]{12}[0-9]{2}$").WithMessage("O CNPJ deve conter 12 caracteres alfanuméricos seguidos por 2 dígitos verificadores numéricos."); // Seguindo o novo padrão de CNPJ alfanumérico
        
        RuleFor(x => x.IsEsgCertified)
            .NotNull().WithMessage("A certificação ESG deve ser informada.");
    }
}