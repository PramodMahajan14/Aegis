using Aegis.DataAccess.Data;
using Aegis.Model.Master;
using Aegis.Services.Services;
using Aegis.Utility.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aegis.Services.Features.MasterManagement.GetJobRoles
{
    public static class GetJobRoles
    {
        // 1. Define the Query (Reads use IRequest instead of Command)
        public class GetJobRolesQuery : IRequest<ApiResponse<object>>
        {
            public Guid OrganizationId { get; set; }

            public GetJobRolesQuery(Guid organizationId)
            {
                OrganizationId = organizationId;
            }
        }

        // 2. Define the Handler
        public class GetJobRolesHandler : IRequestHandler<GetJobRolesQuery, ApiResponse<object>>
        {
            private readonly ApplicationDbContext _context;
            private readonly ILoggingService _logger;

            public GetJobRolesHandler(ApplicationDbContext context, ILoggingService logservice)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
                _logger = logservice ?? throw new ArgumentNullException(nameof(logservice));
            }

            public async Task<ApiResponse<object>> Handle(GetJobRolesQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    // Fetch the list from database filtering by OrganizationId
                    var jobRoles = await _context.JobRoles
                        .Where(x => x.OrganizationId == request.OrganizationId)
                        .Select(x => new JobeRolesVm
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Description = x.Description
                        })
                        .AsNoTracking()
                        .ToListAsync(cancellationToken);

                    _logger.LogInfo("Fetched {Count} job roles for organization {OrganizationId}", jobRoles.Count, request.OrganizationId);

                    return ApiResponse<object>.SuccessResponse(jobRoles, "Job roles retrieved successfully", 200);
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
