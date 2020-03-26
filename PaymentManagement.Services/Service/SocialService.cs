using PaymentManagement.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentManagement.Services.Service
{
    public class SocialService : ISocial
    {
        private decimal rate;
        private decimal contributionAmount;
        public decimal GetSocialDeductions(decimal totalAmount)
        {
            if(totalAmount < 719)
            {
                rate = 0m;
                contributionAmount = 0m;
            }
            else if(totalAmount >= 719 && totalAmount <= 4167)
            {
                rate = .12m;
                contributionAmount = ((totalAmount - 719) * rate);
            }
            else if(totalAmount > 4167)
            {
                rate = .02m;
                contributionAmount = ((4167 - 719) * .12m) + ((totalAmount - 4167) * rate);
            }
            return contributionAmount;
        }
    }
}
