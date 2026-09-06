using System.Data.Common;
using Aegis.DataAccess.Data;
using Aegis.Utility.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Aegis.Services.Features.EmployeeManagement
{
    public static class GetEmployeeDetails
    {
        public class GetEmployeeDetailsQuery : IRequest<ApiResponse<object>>
        {
            public Guid OrganizationId { get; set; }
            public Guid EmployeeId { get; set; }

            public GetEmployeeDetailsQuery(Guid organizationId, Guid employeeId)
            {
                OrganizationId = organizationId;
                EmployeeId = employeeId;
            }
        }

        public class GetEmployeeDetailsHandler : IRequestHandler<GetEmployeeDetailsQuery, ApiResponse<object>>
        {
            private readonly ApplicationDbContext _context;
            private readonly ILogger<GetEmployeeDetailsHandler> _logger;

            public GetEmployeeDetailsHandler(ApplicationDbContext context, ILogger<GetEmployeeDetailsHandler> logger)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            }

            public async Task<ApiResponse<object>> Handle(GetEmployeeDetailsQuery request, CancellationToken cancellationToken)
            {
                if (request.OrganizationId == Guid.Empty)
                {
                    return ApiResponse<object>.ErrorResponse("Organization is required", 400);
                }

                if (request.EmployeeId == Guid.Empty)
                {
                    return ApiResponse<object>.ErrorResponse("EmployeeId is required", 400);
                }

                try
                {
                    var employee = await _context.Employees
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id == request.EmployeeId && x.OrganizationId == request.OrganizationId, cancellationToken);

                    if (employee == null)
                    {
                        return ApiResponse<object>.ErrorResponse("Employee not found", 404);
                    }

                    return ApiResponse<object>.SuccessResponse(employee, "Employee details retrieved successfully");
                }
                catch (DbException dex)
                {
                    _logger.LogError(dex, "Database error occurred while fetching employee details");
                    return ApiResponse<object>.ErrorResponse("Database error occurred", dex.Message, 500);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unexpected error occurred while fetching employee details");
                    return ApiResponse<object>.ErrorResponse("Internal server error", ex.Message, 500);
                }
            }
        }
    }
}
