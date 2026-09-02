using System.Data.Common;
using Aegis.DataAccess.Data;
using Aegis.Model.DTO.MasterDtos;
using Aegis.Model.EmployeeModels;
using Aegis.Model.Master;
using Aegis.Services.Services;
using Aegis.Utility.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aegis.Services.Features.MasterManagement.DeleteJobRole
{
    public static class DeleteJobRole
    {
        public class DeleteJobRoleCommand : IRequest<ApiResponse<object>>
        {
            public Guid Id { get; set; }


            public Guid OrganizationId { get; set; }

            public DeleteJobRoleCommand(Guid organizationId, Guid id)
            {
                Id = id;
                OrganizationId = organizationId;
            }
        }



        public class CreateJobeRoleHandler : IRequestHandler<DeleteJobRoleCommand, ApiResponse<object>>
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



            public async Task<ApiResponse<object>> Handle(DeleteJobRoleCommand request, CancellationToken cancellationToken)
            {


                try
                {
                    var rowsAffected = await _context.JobRoles.Where(x => x.Id == request.Id && x.OrganizationId == request.OrganizationId).ExecuteDeleteAsync();

                    if (rowsAffected == 0)
                    {
                        _logger.LogWarning("Delete failed. Job role ID {Id} not found in organization {OrganizationId}", request.Id, request.OrganizationId);
                        return ApiResponse<object>.ErrorResponse("Job role not found or already deleted!", 404);
                    }
                    _logger.LogInfo("Successfully deleted job role ID {Id} from organization {OrganizationId}", request.Id, request.OrganizationId);

                    return ApiResponse<object>.SuccessResponse(null, "Job role deleted successfully", 200);


                }
                catch (DbException ex)
                {
                    _logger.LogError(ex, "Unexpected erro occured during delete jobrole");
                    return ApiResponse<object>.ErrorResponse("Internal server error", ex.Message, 500);
                }

            }

        }
    }
}