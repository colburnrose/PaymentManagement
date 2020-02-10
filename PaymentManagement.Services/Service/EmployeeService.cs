using PaymentManagement.Entity;
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
            _db.Employees.Add(employee);
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
            throw new NotImplementedException();
        }

        public decimal UnionFees(int id)
        {
            throw new NotImplementedException();
        }
    }
}
