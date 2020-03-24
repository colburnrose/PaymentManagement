using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentManagement.Services.Interface
{
    public interface ITax
    {
        decimal TaxAmount(decimal totalAmount);
    }
}
