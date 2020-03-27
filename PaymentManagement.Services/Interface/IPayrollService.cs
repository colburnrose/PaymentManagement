using Microsoft.AspNetCore.Mvc.Rendering;
using PaymentManagement.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentManagement.Services.Interface
{
    public interface IPayrollService
    {
        Task CreatePaymentAsync(PaymentRecord paymentRecord);
        PaymentRecord GetPaymentById(int id);
        TaxYear GetTaxYearById(int id);
        IEnumerable<PaymentRecord> GetPaymentRecords();
        IEnumerable<SelectListItem> GetTaxYearItems();
        decimal OverTimeHoursWorked(decimal overtimeHours, decimal contractHours); // Overtime hours - Contract hours
        decimal ContractHoursWorked(decimal contractHours, decimal hoursWorked, decimal hourlyRate);
        decimal OvertimeRate(decimal hourlyRate);
        decimal OvertimeEarnings(decimal overtimeRate, decimal overtimeHours);
        decimal TotalEarnings(decimal overtimeEarnings, decimal contractEarnings);
        decimal TotalDeductions(decimal tax, decimal ssn, decimal studentLoan, decimal unionFees);
        decimal GetNetPay(decimal totalEarnings, decimal totalDeductions);
    }
}
