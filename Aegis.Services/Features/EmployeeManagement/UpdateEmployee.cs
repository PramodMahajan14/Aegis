using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using Aegis.DataAccess.Data;
using Aegis.Model.Auth;
using Aegis.Model.DTO.Employee;
using Aegis.Model.Master;
using Aegis.Model.Vm.Employee;
using Aegis.Services.Services;
using Aegis.Utility.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Aegis.Services.Features.EmployeeManagement
{
    public static class UpdateEmployee
    {
        public class UpdateEmployeeCommand : IRequest<ApiResponse<EmployeeVm>>
        {
            public Guid OrganizationId { get; set; }
            public Guid EmployeeId { get; set; }
            public EmployeeDto Model { get; set; }
            public Guid ProjectId { get; set; }

            public UpdateEmployeeCommand(Guid organizationId, Guid employeeId, EmployeeDto model, Guid projectId)
            {
                OrganizationId = organizationId;
                EmployeeId = employeeId;
                Model = model;
                ProjectId = projectId;
            }
        }

        public class UpdateEmployeeHander : IRequestHandler<UpdateEmployeeCommand, ApiResponse<EmployeeVm>>
        {

            private readonly ApplicationDbContext _context;
            private readonly UserManager<ApplicationUser> _userManager;

            private readonly ILoggingService _logger;
            public UpdateEmployeeHander(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILoggingService logging)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
                _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
                _logger = logging ?? throw new ArgumentNullException(nameof(logging));
            }


            public async Task<ApiResponse<EmployeeVm>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
            {
                // =============================================
                // 1. BASIC REQUEST VALIDATION
                // =============================================

                if (request.Model == null)
                {
                    _logger.LogWarning("Employee creation failed: Employee data is required");
                    return ApiResponse<EmployeeVm>.ErrorResponse("Employee data is required", 400);
                }

                if (request.OrganizationId == Guid.Empty)
                {
                    _logger.LogWarning("Employee creation failed: OrganizationId is required");
                    return ApiResponse<EmployeeVm>.ErrorResponse(
                        "Organization is required",
                        400);
                }


                // =============================================
                // 2. DATA ANNOTATION VALIDATION
                // =============================================

                var validationContext =
                    new ValidationContext(request.Model);

                var validationResults =
                    new List<ValidationResult>();

                var isValid = Validator.TryValidateObject(
                    request.Model,
                    validationContext,
                    validationResults,
                    validateAllProperties: true);



                if (!isValid)
                {
                    var errors = validationResults
                        .Select(x => x.ErrorMessage)
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .ToList();

                    _logger.LogWarning(
                        "Employee creation validation failed: {Errors}",
                        string.Join(", ", errors));

                    return ApiResponse<EmployeeVm>.ErrorResponse(
                        "Validation failed",
                        string.Join(", ", errors));
                }


                try
                {
                    // =============================================
                    // 3. FETCH EMPLOYEE
                    // =============================================
                    var employee = await _context.Employees
                        .FirstOrDefaultAsync(x => x.Id == request.EmployeeId && x.OrganizationId == request.OrganizationId);

                    if (employee == null)
                    {
                        return ApiResponse<EmployeeVm>.ErrorResponse("Employee not found", 404);
                    }



                    var jobRole =
                        await _context.JobRoles
                            .AsNoTracking()
                            .FirstOrDefaultAsync(
                                x => x.Id == request.Model.JobRoleId,
                                cancellationToken);


                    if (jobRole == null)
                    {
                        _logger.LogWarning(
                            "Employee updation failed: JobRole not found. " +
                            "JobRoleId={JobRoleId}",
                            request.Model.JobRoleId);


                        return ApiResponse<EmployeeVm>.ErrorResponse(
                            "Job role not found",
                            400);
                    }

                    // =============================================
                    // 4. UPDATE PROPERTIES
                    // =============================================
                    employee.FirstName = request.Model.FirstName.Trim();
                    employee.LastName = request.Model.LastName.Trim();
                    employee.ContactNumber = request.Model.ContactNumber.Trim();
                    employee.DateOfBirth = request.Model.DateOfBirth;
                    employee.JoiningDate = request.Model.JoiningDate;
                    employee.JobRoleId = request.Model.JobRoleId;
                    employee.Gender = request.Model.Gender;

                    _context.Employees.Update(employee);
                    await _context.SaveChangesAsync();


                    var response =
                           new EmployeeVm
                           {

                               Id = employee.Id,

                               FirstName =
                                   employee.FirstName,

                               LastName =
                                   employee.LastName,

                               Email =
                                   employee.Email,

                               JoiningDate =
                                   employee.JoiningDate,

                               ContactNumber =
                                   employee.ContactNumber,

                               DateOfBirth =
                                   employee.DateOfBirth,

                               Gender =
                                   employee.Gender,

                               JobRole =
                                   new BasicJobRoleVm
                                   {
                                       Id = jobRole.Id,

                                       Name = jobRole.Name
                                   },

                               IsActive =
                                   employee.IsActive
                           };


                    _logger.LogInfo(
                        "Employee updated successfully. " +
                        "EmployeeId={EmployeeId}, " +
                        "OrganizationId={OrganizationId}",
                        employee.Id,
                        request.OrganizationId);


                    return ApiResponse<EmployeeVm>
                        .SuccessResponse(
                            response,
                            "Employee created successfully");

                }
                catch (DbException dex)
                {
                    _logger.LogError(
                        dex,
                        "Database error occurred during employee creation");
                    return ApiResponse<EmployeeVm>.ErrorResponse(
                        "Database error occurred",
                        dex.Message,
                        500);
                }

                catch (Exception ex)
                {
                    _logger.LogError(
                       ex,
                       "Unexpected error occurred during employee creation");

                    return ApiResponse<EmployeeVm>.ErrorResponse(
                        "Internal server error",
                        ex.Message,
                        500);
                }

            }
        }
    }
}