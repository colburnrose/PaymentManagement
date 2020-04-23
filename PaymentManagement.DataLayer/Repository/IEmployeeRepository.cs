using Microsoft.AspNetCore.Mvc.Rendering;
using PaymentManagement.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentManagement.DataLayer.Repository
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Employee GetById(int id);
        Task UpdateEmployeeById(int id);
        Task CreateEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeById(int id);
        IEnumerable<Employee> GetAllEmployees();
        decimal UnionFees(int id);
        decimal StudentLoans(int id, decimal amount);
        IEnumerable<SelectListItem> GetAllEmployeesForPayment();
    }
}
