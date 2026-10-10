using Adveshta.DataAccess.Data;
using Adveshta.Services.Services;
using Adveshta.Services.Services.Interfaces;
using Adveshta.Utility.Common;
using Adveshta.Utility.Enum.ProspectEnum;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Adveshta.Services.Features.ContactManagement.DeleteContact
{
    public record DeleteContactCommand(Guid OrganizationId, Guid EmployeeId, Guid ContactId) : IRequest<ApiResponse<object>>;

    public class DeleteContactHandler : IRequestHandler<DeleteContactCommand, ApiResponse<object>>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILoggingService _logger;
        private readonly ITimeLineLogs _timeline;

        public DeleteContactHandler(ApplicationDbContext context, ILoggingService logger, ITimeLineLogs timeline)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _timeline = timeline ?? throw new ArgumentNullException(nameof(timeline));
        }


        public async Task<ApiResponse<object>> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (request.ContactId == Guid.Empty)
                {
                    _logger.LogCritical("Failed to delete contact because ContactId is empty");
                    return ApiResponse<object>.ErrorResponse("Unable to delete contact. Please try again or contact support", null, StatusCodes.Status400BadRequest);
                }

                var contact = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == request.ContactId && x.OrganizationId == request.OrganizationId, cancellationToken);


                if (contact == null)
                {
                    _logger.LogCritical("Failed to delete contact because Contact not found :  {contactId}", request.ContactId);
                    return ApiResponse<object>.ErrorResponse("Unable to delete contact. Please try again or contact support", null, StatusCodes.Status400BadRequest);
                }

                contact.IsActive = false;

                _context.Update(contact);

                _timeline.Log(_context, request.OrganizationId, contact.ProspectId, request.EmployeeId, TimelineEventType.ContactLinked, "Contact was Deleted", $"{contact.FirstName} {contact.LastName} contact deleted", DateTime.UtcNow, TimeLineSouceType.ProspectContact, request.ContactId, null);

                await _context.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                _logger.LogInfo("Contact in-active successfully, {contactId}", request.ContactId);
                return ApiResponse<object>.SuccessResponse(null, "Contact deleted successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogCritical("Contact delete failed,beause {ex} {contactId}", ex.Message, request.ContactId);
                return ApiResponse<object>.ErrorResponse("Internal Server Error", null, StatusCodes.Status500InternalServerError);
            }
        }
    }
}