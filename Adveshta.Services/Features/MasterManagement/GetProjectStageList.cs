using Adveshta.DataAccess.Data;
using Adveshta.Model.DTO;
using Adveshta.Model.Master;
using Adveshta.Services.Services;
using Adveshta.Utility.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Adveshta.Services.Features.MasterManagement
{
    public class GetProjectStageListQuery : IRequest<ApiResponse<object>>
    {
        public Guid OrganizationId { get; set; }

        public GetProjectStageListQuery(Guid organizationdId)
        {
            OrganizationId = organizationdId;

        }
    }

    public class GetProjectStageListHandler : IRequestHandler<GetProjectStageListQuery, ApiResponse<object>>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILoggingService _logger;

        public GetProjectStageListHandler(ApplicationDbContext context, ILoggingService logging)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logging ?? throw new ArgumentNullException(nameof(logging));
        }

        public async Task<ApiResponse<object>> Handle(GetProjectStageListQuery request, CancellationToken cancellationToken)
        {
            try
            {

                List<ProjectStatgeDto> stages = await _context.ProjectStages
                    .AsNoTracking()
                    .Where(x => x.OrganizationId == request.OrganizationId).Select(x=> new ProjectStatgeDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                    })
                    .ToListAsync();
                if (!stages.Any())
                {
                    _logger.LogWarning("Project Stages not found");
                    return ApiResponse<object>.ErrorResponse("Stage not found", null, StatusCodes.Status404NotFound);
                }

                _logger.LogWarning("Project Stages fetched successfully");
                return ApiResponse<object>.SuccessResponse(stages, "Project Stage fetched successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching project stages");
                return ApiResponse<object>.ErrorResponse("Internal server error", null, StatusCodes.Status500InternalServerError);
            }
        }
    }


}