using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class ConstructionRequestValidator : AbstractValidator<ConstructionRequestDTO>
    {
        public ConstructionRequestValidator()
        {
            RuleFor(x => x.MaintenanceServiceId)
                .NotEmpty().WithMessage("Maintenance service id is required.");

            RuleFor(x => x.CustomDesignDescription)
                .NotEmpty().WithMessage("Custom design description is required.");
        }
    }
}
