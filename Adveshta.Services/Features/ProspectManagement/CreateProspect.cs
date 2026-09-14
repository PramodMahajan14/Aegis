using Adveshta.DataAccess.Data;
using Adveshta.Model.DTO.Prospect;
using Adveshta.Model.EmployeeModels;
using Adveshta.Model.ProspectModel;
using Adveshta.Services.Services;
using Adveshta.Utility.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Adveshta.Services.Features.ProspectManagement.CreateProspect
{
    // =========================================================
    // COMMAND
    // =========================================================

    public class CreateProspectCommand : IRequest<ApiResponse<object>>
    {
        public Guid OrganizationId { get; set; }

        public Employee LoggedEmployee { get; set; } = null!;

        public ManageProspectDto Request { get; set; } = null!;

        public CreateProspectCommand(Guid organizationId, Employee employee, ManageProspectDto model)
        {
            OrganizationId = organizationId;
            LoggedEmployee = employee;
            Request = model;
        }
    }

    // =========================================================
    // HANDLER
    // =========================================================

    public class CreateProspectHander : IRequestHandler<CreateProspectCommand, ApiResponse<object>>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILoggingService _logger;

        public CreateProspectHander(ApplicationDbContext context, ILoggingService logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ApiResponse<object>> Handle(
            CreateProspectCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var dto = request.Request;

                // Generate a sequential prospect number: PRO-00001, PRO-00002 …
                var count = await _context.Prospects.CountAsync(cancellationToken);
                var prospectNo = $"PRO-{(count + 1):D5}";

                var prospect = new Prospect
                {
                    Id = Guid.NewGuid(),
                    ProspectNo = prospectNo,
                    OrganizationId = request.OrganizationId,

                    Name = dto.Name.Trim(),
                    BusinessName = dto.BusinessName.Trim(),
                    Description = dto.Description?.Trim(),

                    StatusId = ProspectsStatusMaster.NEW,
                    EstimatedValue = dto.EstimatedValue,
                    ExpectedDecisionDate = dto.ExpectedDecisionDate,

                    ProjectLocation = dto.ProjectLocation.Trim(),
                    OfficeLocation = dto.OfficeLocation?.Trim(),

                    ProspectTemperatureId = dto.TemperatureId,
                    ProspectSourceId = dto.SourceId,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    CreatedById = request.LoggedEmployee.Id,
                };

                _context.Prospects.Add(prospect);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInfo($"Prospect '{prospect.ProspectNo}' created by employee {request.LoggedEmployee.Id}.");

                return ApiResponse<object>.SuccessResponse(
                    new { prospect.Id, prospect.ProspectNo },
                    "Prospect created successfully.",
                    StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating prospect.");

                return ApiResponse<object>.ErrorResponse(
                    "An error occurred while creating the prospect.",
                    null,
                    StatusCodes.Status500InternalServerError);
            }
        }
    }
}