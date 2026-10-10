using Adveshta.DataAccess.Data;
using Adveshta.Model.DTO;
using Adveshta.Model.Master;
using Adveshta.Services.Services;
using Adveshta.Utility.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Adveshta.Services.Features.MasterManagement
{
    public class DeleteProjectStageCommand : IRequest<ApiResponse<object>>
    {
        public Guid OrganizationId { get; set; }
        public Guid Id { get; set; }

        public DeleteProjectStageCommand(Guid organizationdId, Guid ProjectStageId)
        {
            OrganizationId = organizationdId;
            Id = ProjectStageId;
        }
    }

    public class DeleteProjectStageHandler : IRequestHandler<DeleteProjectStageCommand, ApiResponse<object>>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILoggingService _logger;

        public DeleteProjectStageHandler(ApplicationDbContext context, ILoggingService logging)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logging ?? throw new ArgumentNullException(nameof(logging));
        }

        public async Task<ApiResponse<object>> Handle(DeleteProjectStageCommand request, CancellationToken cancellationToken)
        {
            try
            {


                var stage = await _context.ProjectStages
                    .FirstOrDefaultAsync(x => x.Id == request.Id
                     && x.OrganizationId == request.OrganizationId);

                if (stage == null)
                {
                    _logger.LogWarning(
                                            "Project Stage deletion failed: Stage not found. Id: {Id}, OrganizationId: {OrganizationId}",
                                             request.Id,
                                             request.OrganizationId
                                     );
                    return ApiResponse<object>.ErrorResponse("Stage not found", null, StatusCodes.Status400BadRequest);
                }

                _context.ProjectStages.Remove(stage);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInfo(
                               "Project Stage deleted successfully. Id: {Id}",
                               stage.Id
                             );

                return ApiResponse<object>.SuccessResponse(
                    null,
                    "Project stage deleted successfully.",
                    StatusCodes.Status200OK);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while creating Project stage");
                return ApiResponse<object>.ErrorResponse("Internal server error", null, StatusCodes.Status500InternalServerError);
            }
        }
    }


}