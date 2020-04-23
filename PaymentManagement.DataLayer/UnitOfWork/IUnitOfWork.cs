using PaymentManagement.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentManagement.DataLayer.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository EmployeeRepository { get; }
        int Complete();
    }
}
