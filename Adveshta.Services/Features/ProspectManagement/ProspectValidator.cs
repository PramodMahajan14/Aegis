using Adveshta.Model.DTO.Prospect;
using Adveshta.Services.Features.ProspectManagement.CreateProspect;
using FluentValidation;

namespace Adveshta.Services.Features.ProspectManagement.ProspectValidator
{
    // Common validation for both Create and Update
    public class ProspectValidator : AbstractValidator<ManageProspectDto>
    {
        public ProspectValidator()
        {
            // Prospect Name
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Prospect name is required.")
                .MinimumLength(3)
                .WithMessage("Prospect name must be at least 3 characters.")
                .MaximumLength(150)
                .WithMessage("Prospect name must not exceed 150 characters.");

            // Business / Company Name
            RuleFor(x => x.BusinessName)
                .NotEmpty()
                .WithMessage("Company or organisation name is required.")
                .MaximumLength(200)
                .WithMessage("Company name must not exceed 200 characters.");

            // Description - Optional
            RuleFor(x => x.Description)
                .MaximumLength(1000)
                .WithMessage("Description must not exceed 1000 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Description));

            // Status
            RuleFor(x => x.StatusId)
                .NotEmpty()
                .WithMessage("Please select a valid prospect status.");

            // Estimated Value - Optional
            RuleFor(x => x.EstimatedValue)
                .GreaterThan(0)
                .WithMessage("Estimated value must be greater than zero.")
                .When(x => x.EstimatedValue.HasValue);

            // Expected Decision Date - Optional
            RuleFor(x => x.ExpectedDecisionDate)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("Expected decision date must be a future date.")
                .When(x => x.ExpectedDecisionDate.HasValue);

            // Location
            RuleFor(x => x.Location)
                .NotEmpty()
                .WithMessage("Location is required.")
                .MaximumLength(250)
                .WithMessage("Location must not exceed 250 characters.");

            // Office Location - Optional
            RuleFor(x => x.OfficeLocation)
                .MaximumLength(250)
                .WithMessage("Office location must not exceed 250 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.OfficeLocation));

            // Prospect Temperature
            RuleFor(x => x.ProspectTemperatureId)
                .NotEmpty()
                .WithMessage("Please select a prospect temperature (Hot / Warm / Cold).");

            // Prospect Source
            RuleFor(x => x.ProspectSourceId)
                .NotEmpty()
                .WithMessage("Please select a prospect source.");
        }
    }


    // CREATE VALIDATOR
    public class CreateProspectValidator
        : AbstractValidator<CreateProspectCommand>
    {
        public CreateProspectValidator()
        {
            // Validate ManageProspectDto
            RuleFor(x => x.Request)
                .SetValidator(new ProspectValidator());

            // Organization
            RuleFor(x => x.OrganizationId)
                .NotEmpty()
                .WithMessage("Invalid scope. Please log out and log in again.");

            // Logged Employee
            RuleFor(x => x.LoggedEmployee)
                .NotNull()
                .WithMessage("Invalid scope. Please log out and log in again.");
        }
    }


    // UPDATE VALIDATOR
    public class UpdateProspectValidator
        : AbstractValidator<UpdateProspectCommand>
    {
        public UpdateProspectValidator()
        {
            // Validate ManageProspectDto
            RuleFor(x => x.Request)
                .SetValidator(new ProspectValidator());

            // Id is required only for UPDATE
            RuleFor(x => x.Request.Id)
                .NotEmpty()
                .WithMessage("Prospect is required.");

            // Organization
            RuleFor(x => x.OrganizationId)
                .NotEmpty()
                .WithMessage("Invalid scope. Please log out and log in again.");

            // Logged Employee
            RuleFor(x => x.LoggedEmployee)
                .NotNull()
                .WithMessage("Invalid scope. Please log out and log in again.");
        }
    }
}