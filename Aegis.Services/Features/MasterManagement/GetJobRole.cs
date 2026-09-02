using Aegis.DataAccess.Data;
using Aegis.Model.Master;
using Aegis.Services.Services;
using Aegis.Utility.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aegis.Services.Features.MasterManagement.GetJobRole
{
    public static class GetJobRole
    {
        // 1. Define the Query (Reads use IRequest instead of Command)
        public class GetJobRoleQuery : IRequest<ApiResponse<object>>
        {
            public Guid OrganizationId { get; set; }
            public Guid Id { get; set; }

            public GetJobRoleQuery(Guid organizationId, Guid id)
            {
                OrganizationId = organizationId;
                Id = id;
            }
        }

        // 2. Define the Handler
        public class GetJobRolesHandler : IRequestHandler<GetJobRoleQuery, ApiResponse<object>>
        {
            private readonly ApplicationDbContext _context;
            private readonly ILoggingService _logger;

            public GetJobRolesHandler(ApplicationDbContext context, ILoggingService logservice)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
                _logger = logservice ?? throw new ArgumentNullException(nameof(logservice));
            }

            public async Task<ApiResponse<object>> Handle(GetJobRoleQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    // Fetch the list from database filtering by OrganizationId
                    var jobRole = await _context.JobRoles
                        .Where(x => x.OrganizationId == request.OrganizationId && x.Id == request.Id)
                        .Select(x => new JobeRolesVm
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Description = x.Description
                        })
                        .FirstOrDefaultAsync(cancellationToken);

                    if (jobRole == null)
                    {
                        _logger.LogWarning("Job role ID {Id} not found inside organization {OrganizationId}", request.Id, request.OrganizationId);
                        return ApiResponse<object>.ErrorResponse("Job role not found!", 404);
                    }

                    _logger.LogInfo("Fetched job role details for Job role  {joberole} inside  organization {OrganizationId}", request.Id, request.OrganizationId);

                    return ApiResponse<object>.SuccessResponse(jobRole, "Job roles retrieved successfully", 200);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while fetching job roles");
                    return ApiResponse<object>.ErrorResponse("Internal server error", ex.Message, 500);
                }
            }
        }
    }
}
