using Adveshta.Services.Features.ProspectManagement.CreateProspect;
using FluentValidation;

namespace Adveshta.Services.Features.ProspectManagement.ProspectValidator
{
    public class CreateProspectValidator : AbstractValidator<CreateProspectCommand>
    {
        public CreateProspectValidator()
        {
            // ── Scope / Auth guards ───────────────────────────────────────────
            RuleFor(x => x.OrganizationId)
                .NotEmpty()
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid scope. Please log out and log in again.");

            RuleFor(x => x.LoggedEmployee)
                .NotNull()
                .WithMessage("Invalid scope. Please log out and log in again.");

            // ── Prospect Name ─────────────────────────────────────────────────
            RuleFor(x => x.Request.Name)
                .NotEmpty().WithMessage("Prospect name is required.")
                .MinimumLength(3).WithMessage("Prospect name must be at least 3 characters.")
                .MaximumLength(150).WithMessage("Prospect name must not exceed 150 characters.");

            // ── Business / Company Name ───────────────────────────────────────
            RuleFor(x => x.Request.BusinessName)
                .NotEmpty().WithMessage("Company or organisation name is required.")
                .MaximumLength(200).WithMessage("Company name must not exceed 200 characters.");

            // ── Description (optional but bounded) ───────────────────────────
            RuleFor(x => x.Request.Description)
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.")
                .When(x => x.Request.Description != null);

            // ── Status ────────────────────────────────────────────────────────
            RuleFor(x => x.Request.StatusId)
                .NotEmpty()
                .NotEqual(Guid.Empty)
                .WithMessage("Please select a valid prospect status.");

            // ── Estimated Value (optional, but must be positive) ──────────────
            RuleFor(x => x.Request.EstimatedValue)
                .GreaterThan(0).WithMessage("Estimated value must be greater than zero.")
                .When(x => x.Request.EstimatedValue.HasValue);

            // ── Expected Decision Date (optional, but must be in the future) ──
            RuleFor(x => x.Request.ExpectedDecisionDate)
                .GreaterThan(DateTime.UtcNow).WithMessage("Expected decision date must be a future date.")
                .When(x => x.Request.ExpectedDecisionDate.HasValue);

            // ── Location ──────────────────────────────────────────────────────
            RuleFor(x => x.Request.Location)
                .NotEmpty().WithMessage("Location is required.")
                .MaximumLength(250).WithMessage("Location must not exceed 250 characters.");

            // ── Office Location (optional but bounded) ─────────────────────────
            RuleFor(x => x.Request.OfficeLocation)
                .MaximumLength(250).WithMessage("Office location must not exceed 250 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Request.OfficeLocation));

            // ── Prospect Temperature ──────────────────────────────────────────
            RuleFor(x => x.Request.ProspectTemperatureId)
                .NotEmpty()
                .NotEqual(Guid.Empty)
                .WithMessage("Please select a prospect temperature (Hot / Warm / Cold).");

            // ── Prospect Source ───────────────────────────────────────────────
            RuleFor(x => x.Request.ProspectSourceId)
                .NotEmpty()
                .NotEqual(Guid.Empty)
                .WithMessage("Please select a prospect source.");
        }
    }
}
