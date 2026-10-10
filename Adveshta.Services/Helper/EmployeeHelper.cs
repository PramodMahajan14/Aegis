using Adveshta.DataAccess.Data;
using Adveshta.Model.EmployeeModels;
using Microsoft.EntityFrameworkCore;

namespace Adveshta.Services.Helper
{
    public class EmployeeHelper
    {

        public readonly ApplicationDbContext _context;
        public EmployeeHelper(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Employee?> GetEmployeeByUserId(string userId)
        {
            return await _context.Employees.Include(e=>e.Organization).Include(e => e.User).SingleOrDefaultAsync(a => a.UserId == userId && a.IsActive == true);
        }
        public async Task<List<Guid>> GetOrganizationsByEmployeeAsync(Guid employeeId)
        {
            return await _context.EmployeeOrganizations
                .Where(eo => eo.EmployeeId == employeeId)
                .Select(eo => eo.OrganizationId)
                .ToListAsync();
        }

    }
}