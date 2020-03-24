using PaymentManagement.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentManagement.Services.Service
{
    public class TaxService : ITax
    {
        private decimal taxRate;
        private decimal tax;
        public decimal TaxAmount(decimal totalAmount)
        {
            if(totalAmount <= 1042)
            {
                // Free Rate
                taxRate = 0.0m;
                tax = totalAmount * taxRate;
            }
            else if(totalAmount > 1042 && totalAmount < 3125)
            {
                taxRate = 0.20m;
                tax = (1042 * .0m) + ((totalAmount - 1042) * taxRate);
            }
            else if(totalAmount > 3125 && totalAmount < 12500)
            {
                taxRate = 0.40m;
                tax = (1042 * .0m) + ((3125 - 1042) * .20m) + ((totalAmount - 3125) * taxRate);
            }
            else if(totalAmount > 12500)
            {
                taxRate = 0.45m;
                tax = (1042 * .0m) + ((3125 - 1042) * .20m) + ((12500 - 3125) * .40m) + ((totalAmount - 12500) * taxRate);
            }
            return tax;
        }
    }
}
