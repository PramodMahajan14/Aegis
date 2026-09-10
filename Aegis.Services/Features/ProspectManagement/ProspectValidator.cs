using Aegis.Services.Features.ProspectManagement.CreateProspect;
using FluentValidation;

namespace Aegis.Services.Features.ProspectManagement.ProspectValidator
{
    public class CreateProspectValidator : AbstractValidator<CreateProspectCommand>
    {
        public CreateProspectValidator()
        {
            RuleFor(x => x.OrganizationId).NotEmpty().WithMessage("Invalid scope created, Logout first then login");
            RuleFor(x => x.LoggedEmployee).NotEmpty().WithMessage("Invalid scope created, Logout first then login");

            RuleFor(x => x.OrganizationId).NotEmpty().WithMessage("Invalid scope created, Logout first then login");
            RuleFor(x => x.LoggedEmployee).NotEmpty().WithMessage("Invalid scope created, Logout first then login");
            RuleFor(x => x.Request.Name)
          .NotEmpty()
          .WithMessage("Prospect name is required.")
          .MaximumLength(150).WithMessage("Prospect should be less than l 150 charactors")
          .MaximumLength(5).WithMessage("Prospect name should be grather than   5 charactors");

            RuleFor(x => x.Request.BusinessName).NotEmpty().WithMessage("Organization or Company Name is required!");
            RuleFor(x => x.Request.Location).NotEmpty().WithMessage("Location is required");
            RuleFor(x => x.Request.ProspectSourceId).NotEmpty().WithMessage("Prospect sourcr is required");
            RuleFor(x => x.Request.StatusId).NotEmpty().WithMessage("Status is required");
        }
    }
}

