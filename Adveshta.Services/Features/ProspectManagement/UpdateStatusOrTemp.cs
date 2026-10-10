using System.Text.Json;
using Adveshta.DataAccess.Data;
using Adveshta.Model.DTO.Prospect;
using Adveshta.Model.EmployeeModels;
using Adveshta.Model.ProspectModel;
using Adveshta.Services.Services;
using Adveshta.Services.Services.Interfaces;
using Adveshta.Utility.Common;
using Adveshta.Utility.Enum.ProspectEnum;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Adveshta.Services.Features.ProspectManagement.UpdateStatusOrTemp
{
    public class UpdateStatusOrTempCommand : IRequest<ApiResponse<object>>
    {
        public Guid Id { get; set; }
        public Employee LoggedEmployee { get; set; }
        public Guid OrganizationId { get; set; }
        public JsonPatchDocument<ProspectPatchDto> Model { get; set; }

        public UpdateStatusOrTempCommand(Guid prospectId, JsonPatchDocument<ProspectPatchDto> request, Guid organizationId, Employee employee)
        {
            Id = prospectId;
            Model = request;
            OrganizationId = organizationId;
            LoggedEmployee = employee;

        }
    }

    public class UpdateStatusOrTempHandler : IRequestHandler<UpdateStatusOrTempCommand, ApiResponse<object>>
    {
        private readonly ApplicationDbContext _context;
        private readonly ITimeLineLogs _logTime;
        private readonly ILoggingService _logger;

        public UpdateStatusOrTempHandler(ApplicationDbContext context, ITimeLineLogs logTime, ILoggingService logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logTime = logTime ?? throw new ArgumentNullException(nameof(logTime));
        }


        public async Task<ApiResponse<object>> Handle(UpdateStatusOrTempCommand request, CancellationToken cancellationToken)
        {


            if (request.Model == null)
            {
                return ApiResponse<object>.ErrorResponse(
                    "Patch request is required!", null, StatusCodes.Status400BadRequest);
            }
            await using var tranasaction = await _context.Database.BeginTransactionAsync();

            try
            {

                var prospect = await _context.Prospects.FirstOrDefaultAsync(x => x.OrganizationId == request.OrganizationId && x.Id == request.Id);

                if (prospect == null)
                {
                    _logger.LogError("Prospect Not found : Prospect {prospect} Due to Update Status or Temp.");
                    return ApiResponse<object>.ErrorResponse("Prospect not found!", null, StatusCodes.Status400BadRequest);
                }

                // Create a DTO from the existing entity.
                var model = new ProspectPatchDto
                {
                    StatusId = prospect.StatusId,
                    ProspectTemperatureId = prospect.ProspectTemperatureId,
                };
                // Apply the patch to the DTO, not the entity.
                var modelState = new ModelStateDictionary();

                request.Model.ApplyTo(model, error =>
                {
                    modelState.AddModelError(string.Empty, error.ErrorMessage);
                });

                if (!modelState.IsValid)
                {
                    return ApiResponse<object>.ErrorResponse("Invalid  request !", null, StatusCodes.Status400BadRequest);
                }

                // Validate the resulting DTO here.
                // Check that the requested status and temperature
                // exist and are valid for this organization.

                // Update only the permitted properties.
                prospect.StatusId = (Guid)(model.StatusId ?? model.StatusId);

                prospect.ProspectTemperatureId = (Guid)(model.ProspectTemperatureId ?? model.ProspectTemperatureId);


                var metadata = JsonSerializer.Serialize(new
                {
                    Field = model.StatusId.HasValue ? "Status" : "Temperature",

                    From = model.StatusId.HasValue
                                  ? prospect.StatusId
                                   : prospect.ProspectTemperatureId,

                    To = model.StatusId ?? model.ProspectTemperatureId
                });

                string summary = $"Prospect {(model.StatusId.HasValue ? "Status" : "Temperature")} was updated";

                _logTime.Log(
                   _context,
                   request.OrganizationId,
                   prospect.Id,
                   request.LoggedEmployee.Id,
                   TimelineEventType.ProspectCreated,
                   "Prospect Updated",
                   summary,
                   DateTime.UtcNow,
                   null, null,
                   metadata
               );

                await _context.SaveChangesAsync(cancellationToken);
                if (model.StatusId.HasValue)
                {
                    _logger.LogInfo("Prospect status updated : {Prospect} :  {statusId}", prospect.Id, model.StatusId);
                }
                else
                {
                    _logger.LogInfo("Prospect temperature updated : {Prospect} :  {temperature}", prospect.Id, model.ProspectTemperatureId);
                }

                await tranasaction.CommitAsync(cancellationToken);

                return ApiResponse<object>.SuccessResponse(null, $"Prospect {(model.StatusId.HasValue ? "Status" : "Temperature")} changed", StatusCodes.Status200OK);

            }
            catch (Exception ex)
            {
                await tranasaction.RollbackAsync(cancellationToken);
                _logger.LogError("Update  Status Or Tempeture  Request failed : Prospect {prospect} , Due To : {failed}", request.Id, ex.Message);
                return ApiResponse<object>.ErrorResponse("Internal Server Error occured", null, StatusCodes.Status500InternalServerError);
            }
        }
    }
}