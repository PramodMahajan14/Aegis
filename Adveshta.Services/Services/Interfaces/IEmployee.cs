using Adveshta.Model.DTO.Employee;
using Adveshta.Utility.Common;

namespace Adveshta.Services.Services.Interfaces
{
    public interface IEmployee
    {
        Task<ApiResponse<object>> CreateEmployee(EmployeeDto model);

        Task<ApiResponse<object>> UpdateEmployee(EmployeeDto model);

        Task<ApiResponse<object>> GetListEmployee();
    }
}