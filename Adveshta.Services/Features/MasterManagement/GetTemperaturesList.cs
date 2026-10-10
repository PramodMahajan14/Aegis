using Adveshta.DataAccess.Data;
using Adveshta.Model.Master;
using Adveshta.Services.Services;
using Adveshta.Utility.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Adveshta.Services.Features.MasterManagement.GetTemperaturesList
{
        // 1. Define the Query (Reads use IRequest instead of Command)
        public class GetTemperaturesListQuery : IRequest<ApiResponse<object>>
        {

            public GetTemperaturesListQuery()
            {
            }
        }

        // 2. Define the Handler
        public class GetTemperaturesListHandler : IRequestHandler<GetTemperaturesListQuery, ApiResponse<object>>
        {
            private readonly ApplicationDbContext _context;
            private readonly ILoggingService _logger;

            public GetTemperaturesListHandler(ApplicationDbContext context, ILoggingService logservice)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
                _logger = logservice ?? throw new ArgumentNullException(nameof(logservice));
            }

            public async Task<ApiResponse<object>> Handle(GetTemperaturesListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    // Fetch the list from database filtering by OrganizationId
                    var temperature = await _context.ProspectTemperatures
                        .Select(x => new ProspectSourceVm
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Code = x.Code,
                        })
                        .AsNoTracking()
                        .ToListAsync(cancellationToken);

                    _logger.LogInfo("Fetched {Count} Prospect Temperatures ");

                    return ApiResponse<object>.SuccessResponse(temperature, "Job roles retrieved successfully", 200);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while fetching Temperatures");
                    return ApiResponse<object>.ErrorResponse("Internal server error", ex.Message, 500);
                }
            }
        }
}
