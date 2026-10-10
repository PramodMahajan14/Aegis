using Adveshta.Model.DTO;
using FluentValidation;

namespace Adveshta.Services.Features.MasterManagement
{



    public class MasterVlidation : AbstractValidator<BaseMasterDTO>
    {
        public MasterVlidation()
        {
            RuleFor(x => x.Name)
              .NotEmpty()
              .WithMessage("Name is required.")
              .MinimumLength(3)
              .WithMessage("Name must be at least 3 characters long.")
              .MaximumLength(150)
              .WithMessage("Name must not exceed 150 characters.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MinimumLength(5)
                .WithMessage("Description must be at least 5 characters long.")
                .MaximumLength(200)
                .WithMessage("Description must not exceed 200 characters.");


        }

    }


    public class ProjectStageValidation : AbstractValidator<CreateProjectStageCommand>
    {
        public ProjectStageValidation()
        {
            RuleFor(x => x.Model)
             .SetValidator(new MasterVlidation());

            RuleFor(x => x.OrganizationId).NotEmpty()
             .WithMessage("Invalid request !, Please login fisrt");
        }
    }

    public class UpdateProjectStageValidation 
    : AbstractValidator<UpdateProjectStageCommand>
{
    public UpdateProjectStageValidation()
    {
        RuleFor(x => x.Model)
            .SetValidator(new MasterVlidation());

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Project Stage is required.");

        RuleFor(x => x.OrganizationId)
            .NotEmpty()
            .WithMessage("Invalid request. Please login first.");
    }
}

}