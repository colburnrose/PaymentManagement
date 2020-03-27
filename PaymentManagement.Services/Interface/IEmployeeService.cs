using Microsoft.AspNetCore.Mvc.Rendering;
using PaymentManagement.Entity;
using PaymentManagement.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentManagement.Services.Interface
{
    public interface IEmployeeService
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
