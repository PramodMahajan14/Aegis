using System.Runtime.CompilerServices;
using Adveshta.DataAccess.Data;
using Adveshta.Model.DTO.Contacts;
using Adveshta.Services.Services;
using Adveshta.Services.Services.Interfaces;
using Adveshta.Utility.Common;
using Adveshta.Utility.Enum.ProspectEnum;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Adveshta.Services.Features.ContactManagement.UpdateContact
{
    public record UpdateContactCommand(Guid OrganizationId, Guid EmployeeId, ManageContactDto contact) : IRequest<ApiResponse<object>>;

    public class UpdateContactHander : IRequestHandler<UpdateContactCommand, ApiResponse<object>>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILoggingService _logger;
        private readonly ITimeLineLogs _timeline;
        private readonly IMapper _mapper;

        public UpdateContactHander(ApplicationDbContext context, ILoggingService logger, ITimeLineLogs timeline, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _timeline = timeline ?? throw new ArgumentNullException(nameof(timeline));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<ApiResponse<object>> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var contact = request.contact;
                if (!contact.Id.HasValue)
                {
                    _logger.LogError("Contact updation failed due to : contact id missing , contact from prospect : {prospectId}", contact.ProspectId);
                    return ApiResponse<object>.ErrorResponse("Unable to update contact. Please try again or contact support.", null, StatusCodes.Status400BadRequest);
                }

                var prospect = await _context.Prospects.AsNoTracking().FirstOrDefaultAsync(x => x.Id == contact.ProspectId && x.OrganizationId == request.OrganizationId);

                if (prospect == null)
                {
                    _logger.LogError("Contact updation failed due to : Prospect not found : {ProspectId}", contact.ProspectId);
                    return ApiResponse<object>.ErrorResponse("Unable to update contact. Prospect not found.", null, StatusCodes.Status400BadRequest);
                }


                var existenContact = await _context.Contacts
                                  .FirstOrDefaultAsync(
                                   x => x.Id == contact.Id &&
                                   x.OrganizationId == request.OrganizationId &&
                                   x.ProspectId == prospect.Id
                           );

                if (existenContact == null)
                {
                    _logger.LogError("Contact updation failed due to : contact not found : {contactId}", contact.Id);
                    return ApiResponse<object>.ErrorResponse("Unable to update contact. contact not found.", null, StatusCodes.Status404NotFound);
                }

                _mapper.Map(contact, existenContact);

                // replace details of contact
                // existenContact.FirstName = contact.FirstName;
                // existenContact.LastName = contact.LastName;
                // existenContact.Designation = contact.Designation;
                // existenContact.JobRoleId = contact.JobRoleId;
                // existenContact.Email = contact.Email;
                // existenContact.Notes = contact.Notes;


                existenContact.UpdatedById = request.EmployeeId;
                existenContact.UpdateAt = DateTime.UtcNow;

                _context.Update(existenContact);

                _timeline.Log(_context, request.OrganizationId, prospect.Id, request.EmployeeId, TimelineEventType.ContactLinked, "Contact updated",
                                  $"Contact updated for prospect {prospect.Name}", existenContact.UpdateAt, TimeLineSouceType.ProspectContact, contact.Id, null
                             );
                await _context.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                return ApiResponse<object>.SuccessResponse(null, "Contact updated successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError("Contact updation failed due to :  {exception}", ex.Message);
                return ApiResponse<object>.ErrorResponse("Internal server error", ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}