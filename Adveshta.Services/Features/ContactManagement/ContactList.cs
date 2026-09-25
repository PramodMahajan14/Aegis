using Adveshta.DataAccess.Data;
using Adveshta.Model.EmployeeModels;
using Adveshta.Utility.Common;
using MediatR;

namespace Adveshta.Services.Features.ContactManagement.ContactList
{
    public record ContactListQuery(
          Guid OrganizationId,
          Guid EmployeeId,
          Guid? ProspectId
      ) : IRequest<ApiResponse<object>>;

    public class ContactListHandler : IRequestHandler<ContactListQuery, ApiResponse<object>>
    {
        private readonly ApplicationDbContext _context;

        public ContactListHandler(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ApiResponse<object>> handle(ContactListQuery request, CancellationToken cancellationToken)
        {
            try
            {

            }
            catch (Exception ex)
            {
            }
        }
    }
}