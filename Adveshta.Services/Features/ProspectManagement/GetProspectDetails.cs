using Adveshta.DataAccess.Data;
using Adveshta.Model.EmployeeModels;
using Adveshta.Model.Vm.Prospect;
using Adveshta.Services.Services;
using Adveshta.Utility.Common;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Adveshta.Services.Features.ProspectManagement.GetProspectDetails
{

    public class GetProspectDetailsQeury : IRequest<ApiResponse<object>>
    {
        public Guid OrganizationId { get; set; }
        public Guid ProspectId { get; set; }
        public Employee Employee { get; set; } = null!;
        public GetProspectDetailsQeury(Guid organizationId, Employee employee, Guid prospectId)
        {
            OrganizationId = organizationId;
            Employee = employee;
            ProspectId = prospectId;
        }
    }


    public class GetProspectDetailsHandler : IRequestHandler<GetProspectDetailsQeury, ApiResponse<object>>
    {

        private readonly ApplicationDbContext _context;
        private readonly ILoggingService _logger;

        private readonly IMapper _mapper;


        public GetProspectDetailsHandler(ApplicationDbContext context, ILoggingService logger, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ApiResponse<object>> Handle(GetProspectDetailsQeury request, CancellationToken cancellationToken)
        {
            try
            {

                var prospect = await _context.Prospects
                                     .AsNoTracking()
                                     .Include(x => x.ProjectStage)
                                     .Include(x => x.ProspectStatus)
                                     .Include(x => x.ProspectTemperature)
                                    .Include(x => x.CreatedBy)
                                    .Include(x => x.UpdatedBy)
                                     .FirstOrDefaultAsync(x =>
                                                          x.Id == request.ProspectId &&
                                                          x.OrganizationId == request.OrganizationId);

                if (prospect == null)
                {
                    _logger.LogError(" Prospect not found by {ProspectId}", request.ProspectId);
                    return ApiResponse<object>.ErrorResponse("Prospect not found.", null, StatusCodes.Status400BadRequest);
                }

                var result = _mapper.Map<ProspectDetailsVm>(prospect);

                _logger.LogInfo("Prospect  fetched successfully : Organization {Organization}", request.OrganizationId);
                return ApiResponse<object>.SuccessResponse(result, "Prospect List fetched successfully", StatusCodes.Status200OK);

            }
            catch (Exception ex)
            {
                _logger.LogCritical("Error Occured during fetching Prospect details {ProspectId} :  Error {error}", request.ProspectId, ex.Message);
                return ApiResponse<object>.ErrorResponse("Internal Server Error", null, StatusCodes.Status500InternalServerError);
            }
        }
    }



}