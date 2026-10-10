using Adveshta.DataAccess.Data;
using Adveshta.Model.Vm.Prospect;
using Adveshta.Utility.Common;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Adveshta.Services.Features.GetProspectDropdown
{
    public record ProspectDropdownQuery(Guid OrganizationId) : IRequest<ApiResponse<object>>;

    public class ProspectDropdownHander : IRequestHandler<ProspectDropdownQuery, ApiResponse<object>>
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProspectDropdownHander(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }



        public async Task<ApiResponse<object>> Handle(ProspectDropdownQuery request, CancellationToken cancellationToken)
        {
            try
            {

                List<BasicProspectVm> response = await _context.Prospects
                   .AsNoTracking().Where(x => x.OrganizationId == request.OrganizationId)
                   .ProjectTo<BasicProspectVm>(_mapper.ConfigurationProvider).ToListAsync();
                return ApiResponse<object>.SuccessResponse(response, "Prospect List fetched successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return ApiResponse<object>.ErrorResponse("Internal Server Error", null, StatusCodes.Status500InternalServerError);
            }
        }
    }

}