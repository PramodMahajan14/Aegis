using Adveshta.DataAccess.Data;
using Adveshta.Model.Master;
using Adveshta.Services.Services;
using Adveshta.Utility.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Adveshta.Services.Features.MasterManagement.GetSourceList
{
        // 1. Define the Query (Reads use IRequest instead of Command)
        public class GetSourceListQuery : IRequest<ApiResponse<object>>
        {

            public GetSourceListQuery()
            {
            }
        }

        // 2. Define the Handler
        public class GetSourceListHandler : IRequestHandler<GetSourceListQuery, ApiResponse<object>>
        {
            private readonly ApplicationDbContext _context;
            private readonly ILoggingService _logger;

            public GetSourceListHandler(ApplicationDbContext context, ILoggingService logservice)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
                _logger = logservice ?? throw new ArgumentNullException(nameof(logservice));
            }

            public async Task<ApiResponse<object>> Handle(GetSourceListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    // Fetch the list from database filtering by OrganizationId
                    var jobRoles = await _context.ProspectSources
                        .Select(x => new ProspectSourceVm
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Code = x.Code,
                        })
                        .AsNoTracking()
                        .ToListAsync(cancellationToken);

                    _logger.LogInfo("Fetched {Count} Prospect sources ");

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
