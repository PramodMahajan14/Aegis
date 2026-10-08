using Adveshta.DataAccess.Data;
using Adveshta.Model.ContactModel;
using Adveshta.Model.Vm.Employee;
using Adveshta.Services.Services;
using Adveshta.Utility.Common;
using Adveshta.Utility.Enum.MeetingEnum;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Adveshta.Services.Features.EmployeeManagement.LookupList
{
    public record LookupListQuery(Guid organizationId, Guid loggedEmployedId, Guid? ProspectId, string? searchQuery) : IRequest<ApiResponse<object>>;

    public class LookupListHandler : IRequestHandler<LookupListQuery, ApiResponse<object>>
    {

        private readonly ApplicationDbContext _context;

        private readonly ILoggingService _logger;

        public LookupListHandler(ApplicationDbContext context, ILoggingService logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<ApiResponse<object>> Handle(LookupListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<ContactEmployeeVm> employees = _context.Employees.AsNoTracking()
                                                         .Where(x => x.OrganizationId == request.organizationId)
                                                         .Select(x => new ContactEmployeeVm
                                                         {
                                                             Id = x.Id,
                                                             FirstName = x.FirstName,
                                                             LastName = x.LastName,
                                                             ParticipantType = ParticipantType.Employee,
                                                         });
                IQueryable<Contact> contacts = _context.Contacts.AsNoTracking()
                                                        .Where(x => x.OrganizationId == request.organizationId);

                IQueryable<ContactEmployeeVm> contactsquery;
                if (request.ProspectId.HasValue)
                {
                    contactsquery = contacts.Where(x => x.ProspectId == request.ProspectId).Select(x => new ContactEmployeeVm
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        ParticipantType = ParticipantType.Contact,
                    });

                }
                else
                {
                    contactsquery = contacts.Select(x => new ContactEmployeeVm
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        ParticipantType = ParticipantType.Contact,
                    });
                }

                var query = employees.Concat(contactsquery);

                if (!string.IsNullOrWhiteSpace(request.searchQuery))
                {
                    var SearchTerm = request.searchQuery.Trim();

                    query = query.Where(x =>
                                            x.FirstName.Contains(SearchTerm) ||
                                            x.LastName.Contains(SearchTerm)
                                       );


                }

                List<ContactEmployeeVm> response = await query.OrderBy(x => x.FirstName).ThenBy(x => x.LastName).Take(5).ToListAsync(cancellationToken);

                return ApiResponse<object>.SuccessResponse(response, "Lookup list fetch successfully", StatusCodes.Status200OK);

            }
            catch (Exception ex)
            {
                _logger.LogCritical("contact or employee list fetch  failed : {ex}", ex.Message);
                return ApiResponse<object>.ErrorResponse("Internal Server Error", null, StatusCodes.Status500InternalServerError);
            }
        }
    }
}