using Adveshta.DataAccess.Data;
using Adveshta.Helpers.Prospect;
using Adveshta.Model.DTO.Prospect;
using Adveshta.Model.EmployeeModels;
using Adveshta.Services.Services;
using Adveshta.Services.Services.Interfaces;
using Adveshta.Utility.Common;
using Adveshta.Utility.Enum.ProspectEnum;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Adveshta.Services.Features.ProspectManagement
{
    public class UpdateProspectCommand : IRequest<ApiResponse<object>>
    {
        public Guid OrganizationId { get; set; }

        public Employee LoggedEmployee { get; set; }

        public ManageProspectDto Request { get; set; } =  null!;

        public UpdateProspectCommand(Guid organizationId, Employee employee, ManageProspectDto request)
        {
            OrganizationId = organizationId;
            LoggedEmployee = employee;
            Request = request ?? throw new ArgumentNullException(nameof(request));
        }

    }

    public class UpdateProspectHandler : IRequestHandler<UpdateProspectCommand, ApiResponse<object>>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILoggingService _logger;

        private readonly ITimeLineLogs _timeLog;
        private readonly ProspectStatusHelper _prospectStatusHelper;

        public UpdateProspectHandler(ApplicationDbContext context, ILoggingService logger, ProspectStatusHelper prospectStatusHelper, ITimeLineLogs timeLog)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _timeLog = timeLog ?? throw new ArgumentNullException(nameof(timeLog));
            _prospectStatusHelper = prospectStatusHelper ?? throw new ArgumentNullException(nameof(prospectStatusHelper));
        }
        public async Task<ApiResponse<object>> Handle(UpdateProspectCommand request, CancellationToken cancellationToken)
        {

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {

                var dto = request.Request;

                // Check Prospect Exist

                var prospect = await _context.Prospects.FirstOrDefaultAsync(x => x.Id == dto.Id &&
                x.OrganizationId == request.OrganizationId && x.IsActive);

                if (prospect == null)
                {
                    _logger.LogWarning("Prospect Updation Failed: Prospect Not found {ProjectId}", dto.Id);

                    return ApiResponse<object>.ErrorResponse("Prospect Not found!", null, StatusCodes.Status400BadRequest);
                }

        
                // update data

                prospect.Name = dto.Name;
                prospect.BusinessName = dto.BusinessName;
                prospect.EstimatedValue = dto.EstimatedValue;
                prospect.ExpectedDecisionDate = dto.ExpectedDecisionDate;
                prospect.Description = dto.Description;
                prospect.ProspectSourceId = dto.SourceId;
                prospect.ProspectTemperatureId = dto.TemperatureId;
                prospect.ProjectStageId = dto.ProgressId;
                prospect.UpdateAt = DateTime.UtcNow;
                prospect.UpdatedById = request.LoggedEmployee.Id;


                _context.Update(prospect);


                _timeLog.Log(
                    _context,
                    request.OrganizationId,
                    prospect.Id,
                    request.LoggedEmployee.Id,
                    TimelineEventType.ProspectCreated,
                    "Prospect Updated",
                    $"Prospect {prospect.ProspectNo} was created."
                );

                await _context.SaveChangesAsync(cancellationToken);
               await transaction.CommitAsync(cancellationToken);


                _logger.LogInfo("Prospect Updaed successfully : {prospect}", dto.Id);

                return ApiResponse<object>.SuccessResponse(null, "Prospect updated successfully", StatusCodes.Status200OK);

            }
            catch (Exception ex)
            {

                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "Error occured updating Prospect");

                return ApiResponse<object>.ErrorResponse("Errorr occured while Updating Prospect", null, StatusCodes.Status500InternalServerError);
            }

        }
    }



}