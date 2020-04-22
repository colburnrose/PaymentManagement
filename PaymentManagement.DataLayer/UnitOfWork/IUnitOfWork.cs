using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentManagement.DataLayer.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        int Complete();
    }
}
