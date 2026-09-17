using Adveshta.DataAccess.Data;
using Adveshta.Model.DTO;
using Adveshta.Model.Master;
using Adveshta.Services.Services;
using Adveshta.Utility.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Adveshta.Services.Features.MasterManagement
{
    public class UpdateProjectStageCommand : IRequest<ApiResponse<object>>
    {
        public Guid OrganizationId { get; set; }
        public Guid Id {get;set;}
        public ProjectStatgeDto Model { get; set; } = null!;

     

        public UpdateProjectStageCommand(Guid organizationdId, ProjectStatgeDto dto,Guid ProjectStageId)
        {
            OrganizationId = organizationdId;
            Model = dto;
            Id = ProjectStageId;
        }
    }

    public class UpdateProjectStageHandler : IRequestHandler<UpdateProjectStageCommand, ApiResponse<object>>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILoggingService _logger;

        public UpdateProjectStageHandler(ApplicationDbContext context, ILoggingService logging)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logging ?? throw new ArgumentNullException(nameof(logging));
        }

        public async Task<ApiResponse<object>> Handle(UpdateProjectStageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var dto = request.Model;
                string StageName = dto.Name.Trim();
                string StageDecription = dto.Description.Trim();

                var stage = await _context.ProjectStages.FirstOrDefaultAsync(x => x.Id == request.Id
                                                                && x.OrganizationId == request.OrganizationId);

                if (stage == null)
                {
                    _logger.LogWarning("Project Stage Updation Failed : Stage not found {id}", request.Id);
                    return ApiResponse<object>.ErrorResponse("Stage not found", null, StatusCodes.Status400BadRequest);
                }


                
              stage.Name = StageName;
              stage.Description = StageDecription;

                _context.ProjectStages.Update(stage);
                await _context.SaveChangesAsync(cancellationToken);


                _logger.LogWarning("Project Stage created succefully {Id}", stage.Id);
                    return ApiResponse<object>.SuccessResponse(null,"Project Stage updated successfully",StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while creating Project stage");
                return ApiResponse<object>.ErrorResponse("Internal server error", null, StatusCodes.Status500InternalServerError);
            }
        }
    }


}