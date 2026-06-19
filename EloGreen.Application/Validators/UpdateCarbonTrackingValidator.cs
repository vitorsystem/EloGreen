using EloGreen.Application.ViewModels.Request;
using FluentValidation;

namespace EloGreen.Application.Validators;

public class UpdateCarbonTrackingValidator : AbstractValidator<UpdateCarbonTrackingRequest>
{
    public UpdateCarbonTrackingValidator()
    {
        RuleFor(x => x.ActivityDescription)
            .NotEmpty().WithMessage("A descrição da atividade é obrigatória.")
            .MaximumLength(250);

        RuleFor(x => x.CarbonEmitted)
            .GreaterThan(0).WithMessage("A emissão de carbono deve ser maior que zero.");

        RuleFor(x => x.TrackingDate)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Não é possível registrar uma data no futuro.");

        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("O rastreio precisa estar vinculado a um produto válido.");
    }
}