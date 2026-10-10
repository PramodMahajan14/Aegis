using Adveshta.DataAccess.Data;
using Adveshta.Model.Vm.Prospect;
using Adveshta.Services.Services;
using Adveshta.Utility.Common;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Adveshta.Services.Features.ProspectManagement.ProspectList
{
    public class ProspectListQuery : IRequest<ApiResponse<object>>
    {
        public Guid OrganizationId {get;set;}

        public ProspectListQuery(Guid organizationId)
        {
            OrganizationId = organizationId;
        }
    }
    public class ProspectListHandler : IRequestHandler<ProspectListQuery, ApiResponse<object>>
    {

        private readonly ApplicationDbContext _context;
        private readonly ILoggingService _logger;

        private readonly IMapper _mapper;
        public  ProspectListHandler(ApplicationDbContext context, ILoggingService logger, IMapper mapper)
        {
          _context = context ?? throw new ArgumentNullException(nameof(context));
          _logger = logger ?? throw new ArgumentNullException(nameof(logger));
          _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }

        public async Task<ApiResponse<object>> Handle(ProspectListQuery request,CancellationToken cancellationToken)
        {
            try
            {
                var prospects = await _context.Prospects.AsNoTracking()
                                       .Include(x=> x.ProjectStage)
                                       .Include(x=>x.ProspectStatus)
                                       .Include(x=>x.ProspectTemperature)
                                       .Where(x=> x.OrganizationId == request.OrganizationId).ToListAsync(cancellationToken);

               var result = _mapper.Map<List<ProspectListVm>>(prospects);
               _logger.LogInfo("Prospect List fetched successfully : Organization {Organization}", request.OrganizationId);
               return ApiResponse<object>.SuccessResponse(result,"Prospect List fetched successfully",StatusCodes.Status200OK);
                
            }catch(Exception ex)
            {
                 _logger.LogError(ex, $"Error creating prospect.");
                 return ApiResponse<object>.ErrorResponse("Internal Server Error",null,StatusCodes.Status500InternalServerError);
            }
        }
    }
}