
using Adveshta.DataAccess.Data;
using Adveshta.Model.Master;
using Adveshta.Services.Services;
using Adveshta.Utility.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Adveshta.Services.Features.MasterManagement.GetProspectStatus
{
    // 1. Define the Query (Reads use IRequest instead of Command)
    public class GetProspectStatsQuery : IRequest<ApiResponse<object>>
    {

        public GetProspectStatsQuery()
        {
        }
    }

    // 2. Define the Handler
    public class GetSourceListHandler : IRequestHandler<GetProspectStatsQuery, ApiResponse<object>>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILoggingService _logger;

        public GetSourceListHandler(ApplicationDbContext context, ILoggingService logservice)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logservice ?? throw new ArgumentNullException(nameof(logservice));
        }

        public async Task<ApiResponse<object>> Handle(GetProspectStatsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Fetch the list from database filtering by OrganizationId
                var jobRoles = await _context.ProspectsStatus
                    .Select(x => new ProspectSourceVm
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Code = x.Code,
                    })
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                _logger.LogInfo("Fetched {Count} Prospect Status ");

                return ApiResponse<object>.SuccessResponse(jobRoles, "Prospect Status retrieved successfully", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching status");
                return ApiResponse<object>.ErrorResponse("Internal server error", ex.Message, 500);
            }
        }
    }
}
