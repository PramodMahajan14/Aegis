using Adveshta.DataAccess.Data;
using Adveshta.Model.DTO;
using Adveshta.Model.Master;
using Adveshta.Services.Services;
using Adveshta.Utility.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Adveshta.Services.Features.MasterManagement
{
    public class CreateProjectStageCommand : IRequest<ApiResponse<object>>
    {
        public Guid OrganizationId { get; set; }
        public ProjectStatgeDto Model { get; set; } = null!;

        public CreateProjectStageCommand(Guid organizationdId, ProjectStatgeDto dto)
        {
            OrganizationId = organizationdId;
            Model = dto;
        }
    }

    public class CreateProjectStageHandler : IRequestHandler<CreateProjectStageCommand, ApiResponse<object>>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILoggingService _logger;

        public CreateProjectStageHandler(ApplicationDbContext context, ILoggingService logging)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logging ?? throw new ArgumentNullException(nameof(logging));
        }

        public async Task<ApiResponse<object>> Handle(CreateProjectStageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var dto = request.Model;
                string StageName = dto.Name.Trim();
                string StageDecription = dto.Description.Trim();
                var stage = await _context.ProjectStages.AsNoTracking().FirstOrDefaultAsync(x => x.Name == StageName
                                                                && x.OrganizationId == request.OrganizationId);

                if (stage != null)
                {
                    _logger.LogWarning("Project Stage creation Failed : Due to Duplicate Name {name}", StageName);
                    return ApiResponse<object>.ErrorResponse("Duplicate Name not allow", null, StatusCodes.Status400BadRequest);
                }


                var newStage = new ProjectStage
                {
                    Id = Guid.NewGuid(),
                    Name = StageName,
                    Description = StageDecription,
                    OrganizationId = request.OrganizationId
                };

                _context.ProjectStages.Add(newStage);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogWarning("Project Stage created succefully {Id}", newStage.Id);
                    return ApiResponse<object>.SuccessResponse(null,"Project Stage created successfully",StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while creating Project stage");
                return ApiResponse<object>.ErrorResponse("Internal server error", null, StatusCodes.Status500InternalServerError);
            }
        }
    }


}