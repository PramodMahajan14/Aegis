using System.Security.Cryptography.X509Certificates;
using Adveshta.DataAccess.Data;
using Adveshta.Model.DTO.MeetingDto;
using Adveshta.Model.MeetingModel;
using Adveshta.Services.Services;
using Adveshta.Utility.Common;
using Adveshta.Utility.Enum.MeetingEnum;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Adveshta.Services.Features.MeetingManagement.CreateMeeting
{
    public record CreateMeetingCommand(Guid organizationId, Guid LoggedEmployeeId, ManageMeetingDto meeting) : IRequest<ApiResponse<object>>;

    public class CreateMeetingHandler : IRequestHandler<CreateMeetingCommand, ApiResponse<object>>
    {
        private readonly ApplicationDbContext _context;
        public readonly ILoggingService _logger;
        public readonly TimeLineLogsService _timeLogger;

        public readonly IMapper _mapper;

        public CreateMeetingHandler(ApplicationDbContext context, ILoggingService logger, TimeLineLogsService timeLogger, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _timeLogger = timeLogger ?? throw new ArgumentNullException(nameof(timeLogger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<ApiResponse<object>> Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var dto = request.meeting;
                var prospect = await _context.Prospects.FirstOrDefaultAsync(p => p.Id == dto.ProspectId);

                if (prospect == null)
                {
                    _logger.LogError("Meeting created failed : Prospect not found {prospectId}", dto.ProspectId);
                    return ApiResponse<object>.ErrorResponse("Prospect not found", null, StatusCodes.Status400BadRequest);
                }

                var meeting = _mapper.Map<Meeting>(dto);

                meeting.CreatedAt = DateTime.UtcNow;
                meeting.CreatedById = request.LoggedEmployeeId;





                foreach (var participant in dto.MeetingParticipantList)
                {
                    if (!participant.IsActive) continue;

                    var meetingParticipant = new MeetingParticipant
                    {
                        Id = Guid.NewGuid(),
                        MeetingId = meeting.Id,
                    };

                    if (participant.Type == ParticipantType.Employee)
                    {
                        meetingParticipant.EmployeeId = participant.Id;

                    }
                    else if (participant.Type == ParticipantType.Contact)
                    {
                        meetingParticipant.ContactId = participant.Id;
                    }

                    _context.MeetingParticipants.Add(meetingParticipant);

                }

                await _context.SaveChangesAsync();

                return ApiResponse<object>.ErrorResponse(null, "Prospect created sucessfully", StatusCodes.Status201Created);

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError("Meeting created failed : Error {err}", ex.Message);
                return ApiResponse<object>.ErrorResponse("Internal Server Error", ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}