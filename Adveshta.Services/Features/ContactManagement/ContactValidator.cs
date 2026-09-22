using Adveshta.Model.DTO.Contacts;
using FluentValidation;

namespace Adveshta.Services.Features.ContactManagement
{
    public class ContactValidator : AbstractValidator<ManageContactDto>
    {
        public ContactValidator()
        {

            RuleFor(x=>x.FirstName).NotEmpty()
            .WithMessage("Fisrt Name is required")
            .MaximumLength(30)
            .WithMessage("name must not exceed 30 characters")
            .MinimumLength(3).WithMessage("Fisrt name must be at least 3 characters.");

            RuleFor(x=>x.JobRole).NotEmpty()
            .WithMessage("Job role is required")
            .MinimumLength(2)
            .WithMessage("Invalid job name, Job role at least 2 charactors")
            .MaximumLength(20)
            .WithMessage(x=>$"{x.JobRole} should not exceed 20 charactors");

            RuleFor(x=>x.Notes).NotEmpty()
            .WithMessage("JNotes is required")
            .MinimumLength(2)
            .WithMessage("Job role at least 10 charactors")
            .MaximumLength(50)
            .WithMessage(x=>$"{x.Notes} should not exceed 500 charactors");

            RuleFor(x=>x.PhoneNumber).NotEmpty()
            .When(x=>x.IsPrimary && string.IsNullOrWhiteSpace(x.Email))
            .WithMessage("Email or phone number is required for the primary contact.");


           RuleFor(x=>x.Email).NotEmpty()
            .When(x=>x.IsPrimary && string.IsNullOrWhiteSpace(x.PhoneNumber))
            .WithMessage("Email or phone number is required for the primary contact.");


            
        }
    }
}