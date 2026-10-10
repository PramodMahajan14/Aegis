using Adveshta.DataAccess.Data;
using Adveshta.Model.Vm.Contacts;
using Adveshta.Utility.Common;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Adveshta.Services.Features.GetContactDetail
{
    public record ContactDetailQuery(
        Guid Id,
        Guid OrganizationId
    ) : IRequest<ApiResponse<object>>;

    public class ContactDetailHandler
        : IRequestHandler<ContactDetailQuery, ApiResponse<object>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ContactDetailHandler(
            ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ApiResponse<object>> Handle(
            ContactDetailQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var contact = await _context.Contacts
                    .AsNoTracking()
                    .Where(x =>
                        x.OrganizationId == request.OrganizationId &&
                        x.Id == request.Id)
                    .ProjectTo<ContactListVm>(
                        _mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);

                if (contact == null)
                {
                    return ApiResponse<object>.ErrorResponse(
                        "Contact not found",
                        null,
                        StatusCodes.Status404NotFound);
                }

                return ApiResponse<object>.SuccessResponse(
                    contact,
                    "Contact fetched successfully",
                    StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return ApiResponse<object>.ErrorResponse(
                    "Internal Server Error",
                    null,
                    StatusCodes.Status500InternalServerError);
            }
        }
    }
}