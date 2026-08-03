using System.Net.Mail;
using Aegis.DataAccess.Data;
using Aegis.Model.Auth;
using Aegis.Model.DTO.Employee;
using Aegis.Model.Employee;
using Aegis.Services.Services.Interfaces;
using Aegis.Utility.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aegis.Services.Services
{
    public class EmployeeService : IEmployee
    {
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly ApplicationDbContext _context;

        public EmployeeService(UserManager<ApplicationUser> userManager, ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
            _userManger = userManager;
        }


        public async Task<ApiResponse<object>> CreateEmployee(EmployeeDto model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (string.IsNullOrWhiteSpace(model.Email))
            {
                return ApiResponse<object>.ErrorResponse("Invalid Email address", "Please provide valid email address", 404);
            }

            if (model.DateOfBirth == null)
            {
                return ApiResponse<object>.ErrorResponse("BirthDate should be required", "Please provide BirthDate", 404);
            }

            var today = DateTime.UtcNow.Date;
            var age = today.Year - model.DateOfBirth.Year;

            if (model.DateOfBirth.Date > today.AddYears(-age))
            {
                age--;
            }

            if (age < 18)
            {
                return ApiResponse<object>.ErrorResponse(
                    "Employee must be at least 18 years old",
                    "Please provide a valid birth date",
                    400
                );
            }

            try
            {

                var exist = await _userManger.FindByEmailAsync(model.Email);

                if (exist != null)
                {
                    return ApiResponse<object>.ErrorResponse("This emaill address already exist", "Please unique email address", 404);
                }
                var defaultPassword = "User@123";

                var applicationuser = new ApplicationUser
                {
                    UserName = model.Email.Trim(),
                    Email = model.Email.Trim(),
                    FirstName = model.FirstName.Trim(),
                    LastName = model.LastName.Trim(),
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                var result = await _userManger.CreateAsync(applicationuser, defaultPassword);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return ApiResponse<object>.ErrorResponse(
                        "Failed to create user",
                        errors,
                        400
                    );
                }


                var employee = new Employee
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    DateOfBirth = model.DateOfBirth,
                    JoiningDate = model.JoiningDate,
                    Gender = model.Gender,
                    IsActive = true,
                    UserId = applicationuser.Id

                };

                _context.Employees.Add(employee);

                await _context.SaveChangesAsync();
                var employeeResponse = new
                {
                    employee.Id,
                    employee.FirstName,
                    employee.LastName,
                    employee.Email,
                    employee.JoiningDate,
                    employee.Gender,
                    employee.UserId
                };

                return ApiResponse<object>.SuccessResponse(employeeResponse, "Employee created successfully", 201);
            }
            catch (Exception ex)
            {
                return ApiResponse<object>.ErrorResponse("Internal Server Error.", ex.Message, 500);
            }


        }

        public async Task<ApiResponse<object>> UpdateEmployee(EmployeeDto model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (string.IsNullOrWhiteSpace(model.Email))
            {
                return ApiResponse<object>.ErrorResponse("Invalid Email address", "Please provide valid email address", 404);
            }

            if (model.DateOfBirth == null)
            {
                return ApiResponse<object>.ErrorResponse("BirthDate should be required", "Please provide BirthDate", 404);
            }

            var today = DateTime.UtcNow.Date;
            var age = today.Year - model.DateOfBirth.Year;

            if (model.DateOfBirth.Date > today.AddYears(-age))
            {
                age--;
            }

            if (age < 18)
            {
                return ApiResponse<object>.ErrorResponse(
                    "Employee must be at least 18 years old",
                    "Please provide a valid birth date",
                    400
                );
            }

            try
            {

                var exist = await _context.Employees.FirstOrDefaultAsync(x => x.Id == model.Id);

                if (exist == null)
                {
                    return ApiResponse<object>.ErrorResponse("Employee not found", "Invalid request", 404);

                }

                // Load Identity user
                var user = await _userManger.FindByIdAsync(exist.UserId);

                if (user == null)
                {
                    return ApiResponse<object>.ErrorResponse(
                        "User not found",
                        "Identity user does not exist",
                        404);
                }
                // Update Identity fields
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.UserName = model.Email;
                user.NormalizedEmail = model.Email.ToUpper();
                user.NormalizedUserName = model.Email.ToUpper();
                user.UpdatedAt = DateTime.UtcNow;

                var identityResult = await _userManger.UpdateAsync(user);

                if (!identityResult.Succeeded)
                {
                    var errors = string.Join(", ", identityResult.Errors.Select(e => e.Description));

                    return ApiResponse<object>.ErrorResponse(
                        "Failed to update user",
                        errors,
                        400);
                }

                var UpdateAppUser = new ApplicationUser
                {
                    Email = model.Email.Trim(),
                    FirstName = model.FirstName.Trim(),
                    LastName = model.LastName.Trim(),
                    IsActive = true,
                    UpdatedAt = DateTime.UtcNow,
                };

                var result = await _userManger.UpdateAsync(UpdateAppUser);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return ApiResponse<object>.ErrorResponse(
                        "Failed to create user",
                        errors,
                        400
                    );
                }


                // Update Employee table
                exist.FirstName = model.FirstName;
                exist.LastName = model.LastName;
                exist.Email = model.Email;
                exist.DateOfBirth = model.DateOfBirth;
                exist.JoiningDate = model.JoiningDate;
                exist.Gender = model.Gender;
                exist.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return ApiResponse<object>.SuccessResponse(
       exist,
       "Employee updated successfully",
       200);

            }
            catch (Exception ex)
            {
                return ApiResponse<object>.ErrorResponse("Internal Server Error.", ex.Message, 500);
            }
        }

    }
}