using Adveshta.DataAccess.Data;
using Adveshta.Model.DTO.Prospect;
using Adveshta.Services.Services;
using Adveshta.Services.Services.Interfaces;
using Adveshta.Utility.Common;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace Adveshta.Services.Features.ProspectManagement.UpdateStatusOrTemp
{
    public class UpdateStatusOrTempCommand : IRequest<ApiResponse<object>>
    {
        public Guid Id {get;set;}
         public Guid OrganizationId {get;set;}
        public JsonPatchDocument<ProspectPatchDto> Model {get;set;}

        public UpdateStatusOrTempCommand(Guid prospectId, JsonPatchDocument<ProspectPatchDto> request,Guid organizationId)
        {
            Id =prospectId;
            Model = request;
            OrganizationId = organizationId;

        }
    }

    public class UpdateStatusOrTempHandler : IRequestHandler<UpdateStatusOrTempCommand,ApiResponse<object>>
    {
        private readonly ApplicationDbContext _context;
        private readonly ITimeLineLogs _logTime;
        private readonly ILoggingService _logger;

        public UpdateStatusOrTempHandler(ApplicationDbContext context, ITimeLineLogs logTime, ILoggingService logger)
        {
            _context =  context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logTime = logTime ?? throw new ArgumentNullException(nameof(logTime));
        }


        public async Task<ApiResponse<object>> Handle(UpdateStatusOrTempCommand request, CancellationToken cancellationToken)
        {
            await using var tranasaction = await _context.Database.BeginTransactionAsync();

            try
            {

                var prospect = await _context.Prospects.FirstOrDefaultAsync(x=>x.OrganizationId == request.OrganizationId && x.Id == request.Id);

                if(prospect == null)
                {
                     _logger.LogError("Prospect Not found : Prospect {prospect} Due to Update Status or Temp.");
                    return ApiResponse<object>.ErrorResponse("Prospect not found!",null,StatusCodes.Status400BadRequest);
                }

                request.Model.ApplyTo(prospect);
                
            }catch(Exception ex)
            {
                await tranasaction.RollbackAsync(cancellationToken);
                _logger.LogError("Update  Status Or Tempeture  Request failed : Prospect {prospect} , Due To : {failed}",request.Id,ex.Message);
                return ApiResponse<object>.ErrorResponse("Internal Server Error occured",null,StatusCodes.Status500InternalServerError);
            }
        }
    }
}