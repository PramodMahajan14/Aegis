using System.Data.Common;
using Adveshta.DataAccess.Data;
using Adveshta.Model.Master;
using Adveshta.Model.Vm.Employee;
using Adveshta.Utility.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Adveshta.Services.Features.EmployeeManagement
{
    public static class GetEmployeeList
    {
        public class GetEmployeeListQuery : IRequest<ApiResponse<List<EmployeeVm>>>
        {
            public Guid OrganizationId { get; set; }

            public GetEmployeeListQuery(Guid organizationId)
            {
                OrganizationId = organizationId;
            }
        }

        public class GetEmployeeListHandler : IRequestHandler<GetEmployeeListQuery, ApiResponse<List<EmployeeVm>>>
        {
            private readonly ApplicationDbContext _context;
            private readonly ILogger<GetEmployeeListHandler> _logger;

            public GetEmployeeListHandler(ApplicationDbContext context, ILogger<GetEmployeeListHandler> logger)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            }

            public async Task<ApiResponse<List<EmployeeVm>>> Handle(
     GetEmployeeListQuery request,
     CancellationToken cancellationToken)
            {
                // =====================================================
                // 1. VALIDATE ORGANIZATION
                // =====================================================

                if (request.OrganizationId == Guid.Empty)
                {
                    return ApiResponse<List<EmployeeVm>>.ErrorResponse(
                        "Organization is required",
                        400);
                }

                try
                {
                    // =====================================================
                    // 2. GET EMPLOYEES
                    // =====================================================

                    var employees = await _context.Employees
                        .AsNoTracking()
                        .Where(x =>
                            x.OrganizationId == request.OrganizationId &&
                            x.IsActive)
                        .Include(x => x.JobRole)
                        .Select(x => new EmployeeVm
                        {
                            Id = x.Id,

                            FirstName = x.FirstName,

                            LastName = x.LastName,

                            Email = x.Email,

                            JoiningDate = x.JoiningDate,

                            ContactNumber = x.ContactNumber,

                            DateOfBirth = x.DateOfBirth,

                            Gender = x.Gender,

                            IsActive = x.IsActive,
                            IsRoot = x.IsSystem,

                            JobRole = new BasicJobRoleVm
                            {
                                Id = x.JobRole.Id,
                                Name = x.JobRole.Name
                            }
                        })
                        .ToListAsync(cancellationToken);


                    // =====================================================
                    // 3. RETURN RESPONSE
                    // =====================================================

                    return ApiResponse<List<EmployeeVm>>.SuccessResponse(
                        employees,
                        "Employee list retrieved successfully");
                }
                catch (DbException dex)
                {
                    _logger.LogError(
                        dex,
                        "Database error occurred while fetching employee list");

                    return ApiResponse<List<EmployeeVm>>.ErrorResponse(
                        "Database error occurred",
                        dex.Message,
                        500);
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "Unexpected error occurred while fetching employee list");

                    return ApiResponse<List<EmployeeVm>>.ErrorResponse(
                        "Internal server error",
                        ex.Message,
                        500);
                }
            }
        }
    }
}
