using Adveshta.DataAccess.Data;
using Adveshta.Model.ContactModel;
using Adveshta.Model.DTO.Contacts;
using Adveshta.Model.EmployeeModels;
using Adveshta.Services.Services;
using Adveshta.Services.Services.Interfaces;
using Adveshta.Utility.Common;
using Adveshta.Utility.Enum.ProspectEnum;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Adveshta.Services.Features.ContactManagement.CreateContact
{
    public class CreateContactCommand : IRequest<ApiResponse<object>>
    {
        public Guid OrganizationId { get; set; }
        public Employee Employee { get; set; } = null!;

        public ManageContactDto Model { get; set; } = null!;


        public CreateContactCommand(Guid organizationId, Employee employee, ManageContactDto dto)
        {
            OrganizationId = organizationId;
            Employee = employee;
            Model = dto;
        }

    }

    public class CreateContactHandler : IRequestHandler<CreateContactCommand, ApiResponse<object>>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILoggingService _logger;
        private readonly ITimeLineLogs _timelog;

        public CreateContactHandler(ApplicationDbContext context, ILoggingService logger, ITimeLineLogs timelog)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _timelog = timelog ?? throw new ArgumentNullException(nameof(timelog));

        }


        public async Task<ApiResponse<object>> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var dto = request.Model;
                var prospect = await _context.Prospects.AsNoTracking()
                                       .FirstOrDefaultAsync(x => x.Id == dto.ProspectId && x.OrganizationId == request.OrganizationId);

                if (prospect == null)
                {
                    _logger.LogWarning("contact creation failed due to prospect not found : {prospectId}", request.Model.ProspectId);
                    return ApiResponse<object>.ErrorResponse("Prospect not found", null, StatusCodes.Status404NotFound);
                }

                var role = await _context.JobRoles.FirstOrDefaultAsync(x => x.Id == dto.JobRoleId && x.OrganizationId == request.OrganizationId);

                if (role == null)
                {
                    _logger.LogWarning("contact creation failed due to Job role found : {ProjectContactRole}", dto.JobRoleId);
                    return ApiResponse<object>.ErrorResponse("ProjectContactRole not found", null, StatusCodes.Status404NotFound);
                }

                var newcontact = new Contact
                {
                    Id = Guid.NewGuid(),
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Designation = dto.Designation,
                    JobRoleId = role.Id,
                    PhoneNumber = dto.PhoneNumber,
                    Email = dto.Email,
                    IsPrimary = dto.IsPrimary,
                    ProspectId = dto.ProspectId,
                    CreatedAt = DateTime.UtcNow,
                    CreatedById = request.Employee.Id,
                    OrganizationId = request.OrganizationId

                };

                _context.Add(newcontact);

                _timelog.Log(_context, request.OrganizationId,
                  prospect.Id, request.Employee.Id, TimelineEventType.ContactLinked, "New contact created",
                  $"New contact created for prospect {prospect.Name}",
                  newcontact.CreatedAt, TimeLineSouceType.ProspectContact, newcontact.Id, null);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync(cancellationToken);

                _logger.LogInfo("Contact created successful : {contact}", newcontact.ProspectId);

                return ApiResponse<object>.SuccessResponse(null,"Contact created successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError("Contact creation failed : {err}", ex);

                return ApiResponse<object>.ErrorResponse("Internal Server Error", null, StatusCodes.Status500InternalServerError);

            }
        }
    }
}