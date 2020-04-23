using PaymentManagement.DataLayer.Repository;
using PaymentManagement.Web.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentManagement.DataLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IEmployeeRepository _employeeRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }


        public IEmployeeRepository EmployeeRepository
        {
            get { return _employeeRepository = _employeeRepository ?? new EmployeeRepository(_context); }
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
