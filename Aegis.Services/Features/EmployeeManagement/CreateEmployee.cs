using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using Aegis.DataAccess.Data;
using Aegis.Model.Auth;
using Aegis.Model.DTO.Employee;
using Aegis.Model.EmployeeModels;
using Aegis.Services.Helper;
using Aegis.Services.Services;
using Aegis.Utility.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Aegis.Services.Features.EmployeeManagement
{
    public static class CreateEmployee
    {
        public class CreateEmployeeCommand
            : IRequest<ApiResponse<object>>
        {
            public Guid OrganizationId { get; set; }

            public EmployeeDto Model { get; set; }

            public Employee LoggedEmployee { get; set; }


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


        public class CreateEmployeeHandler
            : IRequestHandler<
                CreateEmployeeCommand,
                ApiResponse<object>>
        {
            private readonly ApplicationDbContext _context;

            private readonly UserManager<ApplicationUser> _userManager;

            private readonly ILogger<CreateEmployeeHandler> _logger;

            private readonly UserHelper _helper;


            public CreateEmployeeHandler(
                ApplicationDbContext context,
                UserManager<ApplicationUser> userManager,
                ILogger<CreateEmployeeHandler> logger,
                UserHelper helper)
            {
                _context = context
                    ?? throw new ArgumentNullException(nameof(context));

                _userManager = userManager
                    ?? throw new ArgumentNullException(nameof(userManager));

                _logger = logger
                    ?? throw new ArgumentNullException(nameof(logger));

                _helper = helper
                    ?? throw new ArgumentNullException(nameof(helper));
            }


            public async Task<ApiResponse<object>> Handle(
      CreateEmployeeCommand request,
      CancellationToken cancellationToken)
            {
                // =============================================
                // 1. BASIC REQUEST VALIDATION
                // =============================================

                if (request.Model == null)
                {
                    _logger.LogWarning(
                        "Employee creation failed: Employee data is required");

                    return ApiResponse<object>.ErrorResponse(
                        "Employee data is required",
                        400);
                }

                if (request.OrganizationId == Guid.Empty)
                {
                    _logger.LogWarning(
                        "Employee creation failed: OrganizationId is required");

                    return ApiResponse<object>.ErrorResponse(
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

                    return ApiResponse<object>.ErrorResponse(
                        "Validation failed",
                        string.Join(", ", errors));
                }


                try
                {
                    // =============================================
                    // 3. NORMALIZE DATA
                    // =============================================

                    var email = request.Model.Email
                        .Trim()
                        .ToLowerInvariant();


                    var firstName = request.Model.FirstName.Trim();

                    var lastName = request.Model.LastName.Trim();

                    var contactNumber = request.Model.ContactNumber.Trim();


                    // =============================================
                    // 4. CHECK EMPLOYEE EMAIL EXISTS
                    // =============================================

                    var employeeExists = await _context.Employees
                        .AnyAsync(
                            x => x.OrganizationId == request.OrganizationId &&
                                 x.Email == email,
                            cancellationToken);


                    if (employeeExists)
                    {
                        _logger.LogWarning(
                            "Employee creation failed: Email already exists. Email={Email}, OrganizationId={OrganizationId}",
                            email,
                            request.OrganizationId);

                        return ApiResponse<object>.ErrorResponse(
                            "An employee with this email already exists in this organization",
                            400);
                    }


                    // =============================================
                    // 5. FIND EXISTING APPLICATION USER
                    // =============================================

                    var appUser = await _userManager
                        .FindByEmailAsync(email);


                    // =============================================
                    // 6. START TRANSACTION
                    // =============================================

                    await using var transaction =
                        await _context.Database.BeginTransactionAsync(
                            cancellationToken);

                    try
                    {
                        // =============================================
                        // 7. CREATE APPLICATION USER IF NOT EXISTS
                        // =============================================

                        if (appUser == null)
                        {
                            var user = new ApplicationUser
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
                                var identityErrors = string.Join(
                                    ", ",
                                    createUserResult.Errors.Select(
                                        x => $"{x.Code}: {x.Description}"));


                                _logger.LogWarning(
                                    "Application user creation failed. Email={Email}, Errors={Errors}",
                                    email,
                                    identityErrors);


                                await transaction.RollbackAsync(
                                    cancellationToken);


                                return ApiResponse<object>.ErrorResponse(
                                    "Failed to create application user",
                                    identityErrors);
                            }


                            appUser = user;
                        }


                        // =============================================
                        // 8. CHECK USER ALREADY BELONGS TO ORGANIZATION
                        // =============================================

                        var employeeAlreadyAssigned =
                            await _context.Employees.AnyAsync(
                                x =>
                                    x.OrganizationId == request.OrganizationId &&
                                    x.UserId == appUser.Id,
                                cancellationToken);


                        if (employeeAlreadyAssigned)
                        {
                            await transaction.RollbackAsync(
                                cancellationToken);

                            return ApiResponse<object>.ErrorResponse(
                                "This user is already an employee in this organization",
                                400);
                        }


                        // =============================================
                        // 9. CREATE EMPLOYEE
                        // =============================================

                        var employee = new Employee
                        {
                            Id = Guid.NewGuid(),

                            FirstName = firstName,

                            LastName = lastName,

                            Email = email,

                            DateOfBirth = request.Model.DateOfBirth,

                            JoiningDate = request.Model.JoiningDate,

                            ContactNumber = contactNumber,

                            JobRoleId = request.Model.JobRoleId,

                            Gender = request.Model.Gender,

                            OrganizationId = request.OrganizationId,

                            UserId = appUser.Id,

                            IsActive = true
                        };


                        // =============================================
                        // 10. ADD AND SAVE EMPLOYEE
                        // =============================================

                        _context.Employees.Add(employee);

                        await _context.SaveChangesAsync(
                            cancellationToken);


                        // =============================================
                        // 11. COMMIT
                        // =============================================

                        await transaction.CommitAsync(
                            cancellationToken);


                        _logger.LogInformation(
                            "Employee created successfully. EmployeeId={EmployeeId}, OrganizationId={OrganizationId}",
                            employee.Id,
                            request.OrganizationId);


                        return ApiResponse<object>.SuccessResponse(employee,
                            "Employee created successfully");
                    }
                    catch (OperationCanceledException)
                    {
                        await transaction.RollbackAsync(
                            CancellationToken.None);

                        _logger.LogWarning(
                            "Employee creation request was cancelled. OrganizationId={OrganizationId}",
                            request.OrganizationId);

                        return ApiResponse<object>.ErrorResponse(
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

                    return ApiResponse<object>.ErrorResponse(
                        "Database error occurred",
                        ex.Message,
                        500);
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "Unexpected error occurred during employee creation");

                    return ApiResponse<object>.ErrorResponse(
                        "Internal server error",
                        ex.Message,
                        500);
                }
            }
        }
    }
}