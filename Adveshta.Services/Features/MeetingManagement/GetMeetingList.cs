
using Adveshta.DataAccess.Data;
using Adveshta.Model.MeetingModel;
using Adveshta.Model.Vm.Employee;
using Adveshta.Model.Vm.MeetingVms;
using Adveshta.Utility.Common;
using Adveshta.Utility.Enum.MeetingEnum;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Adveshta.Services.Features.MeetingManagement.GetMeetingList
{
    public record GetMeetingListQuery(
        Guid organizationId,
        Guid loggedEmployeeId,
        MeetingFilter filter
    ) : IRequest<ApiResponse<object>>;

    public class GetMeetingListHandler
        : IRequestHandler<GetMeetingListQuery, ApiResponse<object>>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<GetMeetingListHandler> _logs;
        private readonly IMapper _mapper;

        public GetMeetingListHandler(
            ApplicationDbContext context,
            ILogger<GetMeetingListHandler> log,
            IMapper mapper)
        {
            _context = context
                ?? throw new ArgumentNullException(nameof(context));

            _logs = log
                ?? throw new ArgumentNullException(nameof(log));

            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ApiResponse<object>> Handle(
            GetMeetingListQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var filter = request.filter;

                // 1. Get meetings for the current organization
                IQueryable<Meeting> query = _context.Meetings
                    .AsNoTracking()
                    .Where(x =>
                        x.OrganizationId == request.organizationId);

                // 2. Filter by prospect
                if (filter.ProspectId.HasValue)
                {
                    var prospectId = filter.ProspectId.Value;

                    query = query.Where(x =>
                        x.ProspectId == prospectId);
                }

                // 3. Search by subject
                if (!string.IsNullOrWhiteSpace(filter.SearchQuery))
                {
                    var search = filter.SearchQuery.Trim();

                    query = query.Where(x =>
                        x.Subject.Contains(search));
                }

                // 4. Filter by custom date range
                // From and To are non-nullable DateTime values.
                if (filter.Range is not null)
                {
                    var from = filter.Range.From;
                    var to = filter.Range.To;

                    if (from > to)
                    {
                        return ApiResponse<object>.ErrorResponse(
                            "Invalid date range",
                            "From date cannot be later than To date.",
                            StatusCodes.Status400BadRequest);
                    }

                    query = query.Where(x =>
                        x.When >= from &&
                        x.When <= to);
                }

                // 5. Sort meetings
                query = filter.Sorting switch
                {
                    MeetingSortBy.EarliestFirst =>
                        query.OrderBy(x => x.When)
                             .ThenBy(x => x.Id),

                    MeetingSortBy.LatestFirst =>
                        query.OrderByDescending(x => x.When)
                             .ThenByDescending(x => x.Id),

                    _ =>
                        query.OrderByDescending(x => x.When)
                             .ThenByDescending(x => x.Id)
                };

                // 6. Project and paginate
                var page = Math.Max(1, filter.Page);
                var pageSize = Math.Clamp(filter.Limit, 1, 100);

                var response =
                    await PageList<MeetingVm>.CreateAsync(
                        query.ProjectTo<MeetingVm>(
                            _mapper.ConfigurationProvider),
                        page,
                        pageSize);

                // 7. Get IDs from the current page only
                var meetingIds = response.data
                    .Select(x => x.Id)
                    .ToList();

                // 8. Fetch participants for these meetings
                if (meetingIds.Count > 0)
                {
                    var participants = await _context.MeetingParticipants
                        .AsNoTracking()
                        .Where(x =>
                            meetingIds.Contains(x.MeetingId))
                        .Select(x => new
                        {
                            x.MeetingId,

                            Participant = new ContactEmployeeVm
                            {
                                Id = x.EmployeeId
                                    ?? x.ContactId
                                    ?? Guid.Empty,

                                FirstName =
                                    x.ParticipantType == ParticipantType.Contact
                                        ? (x.Contact != null
                                            ? x.Contact.FirstName
                                            : string.Empty)
                                        : (x.Employee != null
                                            ? x.Employee.FirstName
                                            : string.Empty),

                                LastName =
                                    x.ParticipantType == ParticipantType.Contact
                                        ? (x.Contact != null
                                            ? x.Contact.LastName
                                            : string.Empty)
                                        : (x.Employee != null
                                            ? x.Employee.LastName
                                            : string.Empty),

                                ParticipantType = x.ParticipantType
                            }
                        })
                        .ToListAsync(cancellationToken);

                    // 9. Group participants by meeting ID
                    var participantsByMeeting = participants
                        .GroupBy(x => x.MeetingId)
                        .ToDictionary(
                            group => group.Key,
                            group => group
                                .Select(x => x.Participant)
                                .ToList());

                    // 10. Attach participants to their meetings
                    foreach (var meeting in response.data)
                    {
                        if (participantsByMeeting.TryGetValue(
                            meeting.Id,
                            out var meetingParticipants))
                        {
                            meeting.Participants = meetingParticipants;
                        }
                    }
                }

                // 11. Return paginated meetings with participants
                return ApiResponse<object>.SuccessResponse(
                    response,
                    "Meetings fetched successfully",
                    StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logs.LogError(
                    ex,
                    "Failed to fetch meeting list for organization {OrganizationId}",
                    request.organizationId);

                return ApiResponse<object>.ErrorResponse(
                    "Internal Server Error",
                    ex.Message,
                    StatusCodes.Status500InternalServerError);
            }
        }
    }
}
