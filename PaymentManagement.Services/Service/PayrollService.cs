using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class PayrollService : IPayrollService
    {
        private readonly ApplicationDbContext _db;
        private decimal contractEarnings;
        private decimal overtimeHours;
        public PayrollService(ApplicationDbContext db)
        {
            _db = db;
        }

        public decimal ContractHoursWorked(decimal contractHours, decimal hoursWorked, decimal hourlyRate)
        {
            if(hoursWorked < contractHours)
            {
                contractEarnings = hoursWorked * hourlyRate;
            }
            else
            {
                contractEarnings = contractHours * hourlyRate;
            }

            return contractEarnings;
        }

        public async Task CreatePaymentAsync(PaymentRecord paymentRecord)
        {
            await _db.PaymentRecords.AddAsync(paymentRecord);
            await _db.SaveChangesAsync();
        }

        public decimal GetNetPay(decimal totalEarnings, decimal totalDeductions)
        {
            var netPay = totalEarnings - totalDeductions;
            return netPay;
        }

        public PaymentRecord GetPaymentById(int id)
        {
            return _db.PaymentRecords.Find(id);
        }

        public IEnumerable<PaymentRecord> GetPaymentRecords()
        {
            return _db.PaymentRecords.OrderBy(s => s.EmpId);
        }

        public IEnumerable<SelectListItem> GetTaxYearItems()
        {
            var allTaxes = _db.TaxYears.Select(taxes => new SelectListItem
            {
                Text = taxes.YearOfTax,
                Value = taxes.Id.ToString()
            });

            return allTaxes;
        }

        public decimal OvertimeEarnings(decimal overtimeRate, decimal overtimeHours)
        {
            var overtimeEarnings = overtimeRate * overtimeHours;
            return overtimeEarnings;
        }

        public decimal OverTimeHoursWorked(decimal hoursWorked, decimal contractHours)
        {
           if(hoursWorked <= contractHours)
            {
                overtimeHours = 0.00m;
            }
           else if(hoursWorked > contractHours)
            {
                overtimeHours = hoursWorked - contractHours;
            }
            return overtimeHours;
        }

        public decimal OvertimeRate(decimal hourlyRate)
        {
            var overtimeEarned = hourlyRate * 1.5m;
            return overtimeEarned;
        }

        public decimal TotalDeductions(decimal tax, decimal ssn, decimal studentLoan, decimal unionFees)
        {
            var deductions = tax + ssn + studentLoan + unionFees;
            return deductions;
        }

        public decimal TotalEarnings(decimal overtimeEarnings, decimal contractEarnings)
        {
            var total_earnings = overtimeEarnings + contractEarnings;
            return total_earnings;
        }
    }
}
