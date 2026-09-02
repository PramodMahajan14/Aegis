using System.Data.Common;
using Aegis.DataAccess.Data;
using Aegis.Model.DTO.MasterDtos;
using Aegis.Model.EmployeeModels;
using Aegis.Model.Master;
using Aegis.Services.Services;
using Aegis.Utility.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aegis.Services.Features.MasterManagement.UpdateJobRole
{
    public static class UpdateJobRole
    {
        public class UpdateJobRoleCommand : IRequest<ApiResponse<object>>
        {
            public Guid Id { get; set; }

            public ManageJobRoleDto Model { get; set; }

            public Guid OrganizationId { get; set; }

            public UpdateJobRoleCommand(ManageJobRoleDto model, Guid organizationId,Guid id)
            {
                Id = id;
                Model = model;
                OrganizationId = organizationId;
            }
        }



        public class CreateJobeRoleHandler : IRequestHandler<UpdateJobRoleCommand, ApiResponse<object>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMediator _meditor;
            private readonly ILoggingService _logger;

            public CreateJobeRoleHandler(ApplicationDbContext context, IMediator mediator, ILoggingService logservice)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
                _meditor = mediator ?? throw new ArgumentNullException(nameof(mediator));
                _logger = logservice ?? throw new ArgumentNullException(nameof(logservice));
            }



            public async Task<ApiResponse<object>> Handle(UpdateJobRoleCommand request, CancellationToken cancellationToken)
            {

                if (request.Model.Name == null || request.Model.Description == null)
                {
                    _logger.LogWarning("invalid request : All Field are required");
                    return ApiResponse<object>.ErrorResponse("Invalid request! All Field are required", null, 400);
                }

                try
                {
                    var jobRole = await _context.JobRoles
              .FirstOrDefaultAsync(x => x.Id == request.Model.Id && x.OrganizationId == request.OrganizationId, cancellationToken);

                    if (jobRole == null)
                    {
                        _logger.LogInfo("Fetched job role   {joberole} for update", request.Model.Id);
                        return ApiResponse<object>.ErrorResponse("Job role not found!", 404);
                    }

                    jobRole.Description = request.Model.Description;
                    jobRole.Name = request.Model.Name;

                    _context.Update(jobRole);
                    await _context.SaveChangesAsync();
                    _logger.LogInfo("JobeRole created successfully : For organization {organization}", request.OrganizationId);
                    return ApiResponse<object>.SuccessResponse(jobRole, "Jobe role created successfully", 200);

                }
                catch (DbException ex)
                {
                    _logger.LogError(ex, "Unexpected erro occured during create jobrole");
                    return ApiResponse<object>.ErrorResponse("Internal server error", ex.Message, 500);
                }

            }

        }
    }
}