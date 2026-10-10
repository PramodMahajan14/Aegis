using Adveshta.DataAccess.Data;
using Adveshta.Model.ContactModel;
using Adveshta.Model.EmployeeModels;
using Adveshta.Model.Master;
using Adveshta.Model.Vm.Contacts;
using Adveshta.Utility.Common;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Adveshta.Services.Features.ContactManagement.ContactList
{
    public record ContactListQuery(
          Guid OrganizationId,
          Guid EmployeeId,
          Guid? ProspectId,
          int Page,
          int PageSize
      ) : IRequest<ApiResponse<object>>;

    public class ContactListHandler : IRequestHandler<ContactListQuery, ApiResponse<object>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ContactListHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ApiResponse<object>> Handle(ContactListQuery request, CancellationToken cancellationToken)
        {

            try
            {
                IQueryable<Contact> query = _context.Contacts.AsNoTracking();

                query = query.Where(x => x.OrganizationId == request.OrganizationId && x.IsActive == true);

                if (request.ProspectId.HasValue)
                {
                    query = query.Where(x => x.ProspectId == request.ProspectId);
                }

                // IQueryable<ContactListVm> contactQuery = query.Select(x => new ContactListVm
                // {
                //     Id = x.Id,
                //     FirstName = x.FirstName,
                //     LastName = x.LastName,
                //     Designation = x.Designation,
                //     IsPrimary = x.IsPrimary,
                //     PhoneNumber = x.PhoneNumber,
                //     Email = x.Email,
                //     CreatedAt = x.CreatedAt,
                //     JobRole = new BasicJobRoleVm
                //     {
                //         Id = x.ProjectContactRole.Id,
                //         Name = x.ProjectContactRole.Name,
                //     },
                //     CreatedBy = new BasicEmployeeVm
                //     {
                //         Id = x.CreatedBy.Id,
                //         FirstName = x.CreatedBy.FirstName,
                //         LastName = x.CreatedBy.LastName
                //     },
                //     Prospect = new BasicProspectVm
                //     {
                //         Id = x.Prospect.Id,
                //         Name = x.Prospect.Name
                //     }

                // });

                IQueryable<ContactListVm> contactQuery = query.ProjectTo<ContactListVm>(_mapper.ConfigurationProvider);

                var reponse = await PageList<ContactListVm>.CreateAsync(contactQuery, request.Page, request.PageSize);

                return ApiResponse<object>.SuccessResponse(reponse, "Contact List featched suucessfuuly", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return ApiResponse<object>.ErrorResponse("Internal Error Occured", ex, StatusCodes.Status500InternalServerError);
            }
        }
    }
}