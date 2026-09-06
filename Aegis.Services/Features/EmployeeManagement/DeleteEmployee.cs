using System.Data.Common;
using Aegis.DataAccess.Data;
using Aegis.Utility.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Aegis.Services.Features.EmployeeManagement
{
    public static class DeleteEmployee
    {
        public class DeleteEmployeeCommand : IRequest<ApiResponse<object>>
        {
            public Guid OrganizationId { get; set; }
            public Guid EmployeeId { get; set; }

            public DeleteEmployeeCommand(Guid organizationId, Guid employeeId)
            {
                OrganizationId = organizationId;
                EmployeeId = employeeId;
            }
        }

        public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, ApiResponse<object>>
        {
            private readonly ApplicationDbContext _context;
            private readonly ILogger<DeleteEmployeeHandler> _logger;

            public DeleteEmployeeHandler(ApplicationDbContext context, ILogger<DeleteEmployeeHandler> logger)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            }

            public async Task<ApiResponse<object>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
            {
                if (request.OrganizationId == Guid.Empty)
                {
                    _logger.LogWarning("Employee deletion failed: OrganizationId is required");
                    return ApiResponse<object>.ErrorResponse("Organization is required", 400);
                }

                if (request.EmployeeId == Guid.Empty)
                {
                    _logger.LogWarning("Employee deletion failed: EmployeeId is required");
                    return ApiResponse<object>.ErrorResponse("EmployeeId is required", 400);
                }

                try
                {
                    var employee = await _context.Employees
                        .FirstOrDefaultAsync(x => x.Id == request.EmployeeId && x.OrganizationId == request.OrganizationId, cancellationToken);

                    if (employee == null)
                    {
                        return ApiResponse<object>.ErrorResponse("Employee not found", 404);
                    }

                    employee.IsActive = false;
                    _context.Employees.Update(employee);
                    await _context.SaveChangesAsync(cancellationToken);

                    return ApiResponse<object>.SuccessResponse(null, "Employee deleted successfully");
                }
                catch (DbException dex)
                {
                    _logger.LogError(dex, "Database error occurred during employee deletion");
                    return ApiResponse<object>.ErrorResponse("Database error occurred", dex.Message, 500);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unexpected error occurred during employee deletion");
                    return ApiResponse<object>.ErrorResponse("Internal server error", ex.Message, 500);
                }
            }
        }
    }
}
