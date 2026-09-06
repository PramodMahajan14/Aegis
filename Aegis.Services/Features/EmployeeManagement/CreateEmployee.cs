
using System.ComponentModel.DataAnnotations;
using System.Data.Common;

using Aegis.DataAccess.Data;
using Aegis.Model.Auth;
using Aegis.Model.DTO.Employee;
using Aegis.Model.EmployeeModels;
using Aegis.Model.Master;
using Aegis.Model.Vm.Employee;
using Aegis.Services.Services;
using Aegis.Utility.Common;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Aegis.Services.Features.EmployeeManagement
{
    public static class CreateEmployee
    {
        // =========================================================
        // COMMAND
        // =========================================================

        public class CreateEmployeeCommand
            : IRequest<ApiResponse<EmployeeVm>>
        {
            public Guid OrganizationId { get; set; }

            public EmployeeDto Model { get; set; } = null!;

            public Employee LoggedEmployee { get; set; } = null!;

            public CreateEmployeeCommand(
                Guid organizationId,
                EmployeeDto model,
                Employee loggedEmployee)
            {
                OrganizationId = organizationId;
                Model = model;
                LoggedEmployee = loggedEmployee;
            }
        }


        // =========================================================
        // HANDLER
        // =========================================================

        public class CreateEmployeeHandler
            : IRequestHandler<
                CreateEmployeeCommand,
                ApiResponse<EmployeeVm>>
        {
            private readonly ApplicationDbContext _context;

            private readonly UserManager<ApplicationUser> _userManager;

            private readonly ILoggingService _logger;


            public CreateEmployeeHandler(
                ApplicationDbContext context,
                UserManager<ApplicationUser> userManager,
                ILoggingService logger)
            {
                _context = context
                    ?? throw new ArgumentNullException(nameof(context));

                _userManager = userManager
                    ?? throw new ArgumentNullException(nameof(userManager));

                _logger = logger
                    ?? throw new ArgumentNullException(nameof(logger));
            }


            // =========================================================
            // HANDLE
            // =========================================================

            public async Task<ApiResponse<EmployeeVm>> Handle(
                CreateEmployeeCommand request,
                CancellationToken cancellationToken)
            {
                try
                {
                    // =================================================
                    // 1. BASIC VALIDATION
                    // =================================================

                    if (request.Model == null)
                    {
                        _logger.LogWarning(
                            "Employee creation failed: Employee data is required");

                        return ApiResponse<EmployeeVm>.ErrorResponse(
                            "Employee data is required",
                            400);
                    }


                    if (request.OrganizationId == Guid.Empty)
                    {
                        _logger.LogWarning(
                            "Employee creation failed: OrganizationId is required");

                        return ApiResponse<EmployeeVm>.ErrorResponse(
                            "Organization is required",
                            400);
                    }


                    // =================================================
                    // 2. DATA ANNOTATION VALIDATION
                    // =================================================

                    var validationContext =
                        new ValidationContext(request.Model);

                    var validationResults =
                        new List<ValidationResult>();

                    var isValid =
                        Validator.TryValidateObject(
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


                    // =================================================
                    // 3. NORMALIZE DATA
                    // =================================================

                    var email = request.Model.Email
                        .Trim()
                        .ToLowerInvariant();

                    var firstName =
                        request.Model.FirstName.Trim();

                    var lastName =
                        request.Model.LastName.Trim();

                    var contactNumber =
                        request.Model.ContactNumber?.Trim()
                        ?? string.Empty;


                    // =================================================
                    // 4. CHECK DUPLICATE EMAIL IN ORGANIZATION
                    // =================================================

                    var employeeExists =
                        await _context.Employees.AnyAsync(
                            x =>
                                x.OrganizationId == request.OrganizationId &&
                                x.Email == email,
                            cancellationToken);


                    if (employeeExists)
                    {
                        _logger.LogWarning(
                            "Employee creation failed: Email already exists. " +
                            "Email={Email}, OrganizationId={OrganizationId}",
                            email,
                            request.OrganizationId);


                        return ApiResponse<EmployeeVm>.ErrorResponse(
                            "An employee with this email already exists in this organization",
                            400);
                    }


                    // =================================================
                    // 5. GET JOB ROLE
                    // =================================================

                    var jobRole =
                        await _context.JobRoles
                            .AsNoTracking()
                            .FirstOrDefaultAsync(
                                x => x.Id == request.Model.JobRoleId,
                                cancellationToken);


                    if (jobRole == null)
                    {
                        _logger.LogWarning(
                            "Employee creation failed: JobRole not found. " +
                            "JobRoleId={JobRoleId}",
                            request.Model.JobRoleId);


                        return ApiResponse<EmployeeVm>.ErrorResponse(
                            "Job role not found",
                            400);
                    }


                    // =================================================
                    // 6. FIND EXISTING APPLICATION USER
                    // =================================================

                    var appUser =
                        await _userManager.FindByEmailAsync(email);


                    // =================================================
                    // 7. START TRANSACTION
                    // =================================================

                    await using var transaction =
                        await _context.Database
                            .BeginTransactionAsync(cancellationToken);


                    try
                    {
                        // =============================================
                        // 8. CREATE APPLICATION USER IF NOT EXISTS
                        // =============================================

                        if (appUser == null)
                        {
                            var user =
                                new ApplicationUser
                                {
                                    UserName = email,

                                    Email = email,

                                    FirstName = firstName,

                                    LastName = lastName,

                                    IsActive = true,

                                    CreatedAt = DateTime.UtcNow
                                };


                            var createUserResult =
                                await _userManager.CreateAsync(
                                    user,
                                    SystemConfigInstance.Password);


                            if (!createUserResult.Succeeded)
                            {
                                var identityErrors =
                                    string.Join(
                                        ", ",
                                        createUserResult.Errors.Select(
                                            x =>
                                                $"{x.Code}: {x.Description}"));


                                _logger.LogWarning(
                                    "Application user creation failed. " +
                                    "Email={Email}, Errors={Errors}",
                                    email,
                                    identityErrors);


                                await transaction.RollbackAsync(
                                    CancellationToken.None);


                                return ApiResponse<EmployeeVm>.ErrorResponse(
                                    "Failed to create application user",
                                    identityErrors);
                            }


                            appUser = user;
                        }


                        // =============================================
                        // 9. CHECK USER ALREADY BELONGS TO ORGANIZATION
                        // =============================================

                        var employeeAlreadyAssigned =
                            await _context.Employees.AnyAsync(
                                x =>
                                    x.OrganizationId ==
                                        request.OrganizationId
                                    &&
                                    x.UserId == appUser.Id,
                                cancellationToken);


                        if (employeeAlreadyAssigned)
                        {
                            await transaction.RollbackAsync(
                                CancellationToken.None);


                            return ApiResponse<EmployeeVm>.ErrorResponse(
                                "This user is already an employee in this organization",
                                400);
                        }


                        // =============================================
                        // 10. CREATE EMPLOYEE
                        // =============================================

                        var employee =
                            new Employee
                            {
                                Id = Guid.NewGuid(),

                                FirstName = firstName,

                                LastName = lastName,

                                Email = email,

                                DateOfBirth =
                                    request.Model.DateOfBirth,

                                JoiningDate =
                                    request.Model.JoiningDate,

                                ContactNumber =
                                    contactNumber,

                                JobRoleId =
                                    request.Model.JobRoleId,

                                Gender =
                                    request.Model.Gender,

                                OrganizationId =
                                    request.OrganizationId,

                                UserId =
                                    appUser.Id,

                                IsActive = true
                            };


                        // =============================================
                        // 11. ADD EMPLOYEE
                        // =============================================

                        _context.Employees.Add(employee);


                        await _context.SaveChangesAsync(
                            cancellationToken);


                        // =============================================
                        // 12. COMMIT TRANSACTION
                        // =============================================

                        await transaction.CommitAsync(
                            cancellationToken);


                   
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
                            "Employee created successfully. " +
                            "EmployeeId={EmployeeId}, " +
                            "OrganizationId={OrganizationId}",
                            employee.Id,
                            request.OrganizationId);


                        return ApiResponse<EmployeeVm>
                            .SuccessResponse(
                                response,
                                "Employee created successfully");
                    }
                    catch (OperationCanceledException)
                    {
                        await transaction.RollbackAsync(
                            CancellationToken.None);


                        _logger.LogWarning(
                            "Employee creation request was cancelled. " +
                            "OrganizationId={OrganizationId}",
                            request.OrganizationId);


                        return ApiResponse<EmployeeVm>.ErrorResponse(
                            "Request cancelled",
                            "Request was cancelled by client",
                            499);
                    }
                    catch
                    {
                        await transaction.RollbackAsync(
                            CancellationToken.None);

                        throw;
                    }
                }
                catch (DbException ex)
                {
                    _logger.LogError(
                        ex,
                        "Database error occurred during employee creation");


                    return ApiResponse<EmployeeVm>.ErrorResponse(
                        "Database error occurred",
                        ex.Message,
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

