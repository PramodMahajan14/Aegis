using Aegis.Model.DTO.Employee;
using Aegis.Utility.Common;

namespace Aegis.Services.Services.Interfaces
{
    public interface IEmployee
    {
        Task<ApiResponse<object>> CreateEmployee(EmployeeDto model);

        Task<ApiResponse<object>> UpdateEmployee(EmployeeDto model);
    }
}