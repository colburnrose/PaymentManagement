using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentManagement.Services.Interface
{
    public interface ISocial
    {
        decimal GetSocialDeductions(decimal totalAmount);
    }
}
