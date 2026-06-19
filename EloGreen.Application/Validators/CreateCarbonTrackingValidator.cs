using EloGreen.Application.ViewModels.Request;
using FluentValidation;

namespace EloGreen.Application.Validators;

public class CreateCarbonTrackingValidator : AbstractValidator<CreateCarbonTrackingRequest>
{
    public CreateCarbonTrackingValidator()
    {
        RuleFor(x => x.ActivityDescription)
            .NotEmpty().WithMessage("A descrição da atividade é obrigatória.")
            .MaximumLength(250).WithMessage("A descrição não pode exceder 250 caracteres.");

        RuleFor(x => x.CarbonEmitted)
            .GreaterThan(0).WithMessage("A emissão de carbono deve ser maior que zero.");

        RuleFor(x => x.TrackingDate)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Não é possível registrar uma data de rastreio no futuro.");

        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("O rastreio precisa estar vinculado a um produto válido.");
    }
}