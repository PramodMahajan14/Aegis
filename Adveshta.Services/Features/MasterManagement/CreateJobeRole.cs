using System.Data.Common;
using Adveshta.DataAccess.Data;
using Adveshta.Model.DTO.MasterDtos;
using Adveshta.Model.EmployeeModels;
using Adveshta.Model.Master;
using Adveshta.Services.Services;
using Adveshta.Utility.Common;
using MediatR;

namespace Adveshta.Services.Features.MasterManagement.CreateJobRole
{
    public static class CreateJobeRole
    {
        public class CreateJobeRoleCommand : IRequest<ApiResponse<object>>
        {
            public Employee LoggedEmployee { get; set; }

            public ManageJobRoleDto Model { get; set; }

            public Guid OrganizatinId { get; set; }

            public CreateJobeRoleCommand(Employee employee, ManageJobRoleDto model, Guid organizatinId)
            {
                LoggedEmployee = employee;
                Model = model;
                OrganizatinId = organizatinId;
            }
        }



        public class CreateJobeRoleHandler : IRequestHandler<CreateJobeRoleCommand, ApiResponse<object>>
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



            public async Task<ApiResponse<object>> Handle(CreateJobeRoleCommand request, CancellationToken cancellationToken)
            {

                if (request.Model.Name == null || request.Model.Description == null)
                {
                    _logger.LogWarning("invalid request : All Field are required");
                    return ApiResponse<object>.ErrorResponse("Invalid request! All Field are required", null, 400);
                }

                try
                {
                    var newJobRole = new JobRole
                    {
                        Id = Guid.NewGuid(),
                        Name = request.Model.Name,
                        Description = request.Model.Description,
                        OrganizationId = request.OrganizatinId,
                    };

                    _context.Add(newJobRole);
                    await _context.SaveChangesAsync();
                    _logger.LogInfo("JobeRole created successfully : For organization {organization}", request.OrganizatinId);
                    return ApiResponse<object>.SuccessResponse(newJobRole, "Jobe role created successfully", 201);

                }
                catch (DbException ex)
                {
                    _logger.LogError(ex, "Unexpected erro occured during create jobrole");
                    return ApiResponse<object>.ErrorResponse("Internal server error", ex.Message, 500);
                }

            }

        }
    }
}