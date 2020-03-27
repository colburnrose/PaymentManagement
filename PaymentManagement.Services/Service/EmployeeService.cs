using Microsoft.AspNetCore.Mvc.Rendering;
using PaymentManagement.Entity;
using PaymentManagement.Entity.Enums;
using PaymentManagement.Services.Interface;
using PaymentManagement.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentManagement.Services.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _db;
        private decimal repayment;
        public EmployeeService(ApplicationDbContext db)
        {
            _db = db;
        }

        public Employee GetById(int id)
        {
            return _db.Employees.Where(x => x.Id == id).FirstOrDefault();
        }
        public async Task CreateEmployeeAsync(Employee employee)
        {
            await _db.Employees.AddAsync(employee);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteEmployeeById(int id)
        {
            var employee = GetById(id);
            _db.Employees.Remove(employee);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            _db.Employees.Update(employee);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateEmployeeById(int id)
        {
            var employee = GetById(id);
            _db.Employees.Update(employee);
            await _db.SaveChangesAsync();
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _db.Employees.ToList();
        }

        public decimal StudentLoans(int id, decimal amount)
        {
            var emp = GetById(id);
            if(emp.StudentLoan == StudentLoan.Yes && amount > 1750 && amount < 2000)
            {
                repayment = 15m;
            }
            else if(emp.StudentLoan == StudentLoan.Yes && amount >= 2000 && amount < 2250)
            {
                repayment = 38m;
            }
            else if (emp.StudentLoan == StudentLoan.Yes && amount >= 2250 && amount < 2500)
            {
                repayment = 60m;
            }
            else if (emp.StudentLoan == StudentLoan.Yes && amount > 2500)
            {
                repayment = 83m;
            }
            else
            {
                repayment = 0m;
            }
            return repayment;
        }

        public decimal UnionFees(int id)
        {
            var emp = GetById(id);
            var fee = emp.UnionMember == UnionMember.Yes ? 10m : 0m;
            return fee;
        }

        public IEnumerable<SelectListItem> GetAllEmployeesForPayment()
        {
            return GetAllEmployees().Select(emp => new SelectListItem()
            {
                Text = emp.FullName,
                Value = emp.Id.ToString()
            }); ;
        }
    }
}
