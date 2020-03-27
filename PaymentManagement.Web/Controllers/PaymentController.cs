using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaymentManagement.Services.Interface;
using PaymentManagement.Web.Models;

namespace PaymentManagement.Web.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPayrollService _payrollService;

        public PaymentController(IPayrollService payrollService)
        {
            _payrollService = payrollService;
        }

        [Route("api/Payment/Index")]
        public IActionResult Index()
        {
            var payments = _payrollService.GetPaymentRecords().Select(payment => new PaymentViewModel
            {
                Id = payment.Id,
                EmployeeId = payment.EmpId,
                FullName = payment.FullName,
                PayDate = payment.DatePaid,
                PayMonth = payment.PayMonth,
                TaxYearId = payment.TaxYearId,
                Year = payment.TaxYear.ToString(),
                TotalEarnings = payment.TotalEarnings,
                TotalDeductions = payment.TotalDeduction,
                NetPayment = payment.NetPayment
            }).ToList();

            return View(payments);
        }
    }
}