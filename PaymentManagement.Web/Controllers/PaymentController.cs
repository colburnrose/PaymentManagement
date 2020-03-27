using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaymentManagement.Entity;
using PaymentManagement.Services.Interface;
using PaymentManagement.Web.Models;

namespace PaymentManagement.Web.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPayrollService _payrollService;
        private readonly IEmployeeService _employeeService;
        private readonly ITax _taxService;
        private readonly ISocial _socialService;
        private decimal overtimeHrs;
        private decimal contractEarnings;
        private decimal overtimeEarnings;
        private decimal totalEarnings;
        private decimal unionFees;
        private decimal ssn;
        private decimal studentLoans;
        private decimal tax;
        private decimal totalDeductions;

        public PaymentController(IPayrollService payrollService, IEmployeeService employeeService, ITax taxService, ISocial socialService)
        {
            _payrollService = payrollService;
            _employeeService = employeeService;
            _taxService = taxService;
            _socialService = socialService;
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
                Year = _payrollService.GetTaxYearById(payment.TaxYearId).YearOfTax,
                TotalEarnings = payment.TotalEarnings,
                TotalDeductions = payment.TotalDeduction,
                NetPayment = payment.NetPayment
            }).ToList();

            return View(payments);
        }

        [HttpGet]
        [Route("api/Payment/Create")]
        public IActionResult Create()
        {
            ViewBag.employees = _employeeService.GetAllEmployeesForPayment();
            ViewBag.taxYears = _payrollService.GetTaxYearItems();
            var model = new PaymentCreateModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/Payment/Create")]
        public async Task<IActionResult> Create(PaymentCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var payment = new PaymentRecord
                {
                    Id = model.Id,
                    EmpId = model.EmpId,
                    Employee = model.Employee,
                    FullName = _employeeService.GetById(model.EmpId).FullName,
                    NINO = _employeeService.GetById(model.EmpId).SSN,
                    DatePaid = model.DatePaid,
                    PayMonth = model.PayMonth,
                    TaxYearId = model.TaxYearId,
                    TaxCode = model.TaxCode,
                    HourlyRate = model.HourlyRate,
                    HoursWorked = model.HoursWorked,
                    ContractHours = model.ContractHours,
                    OvertimeHours = overtimeHrs = _payrollService.OverTimeHoursWorked(model.HoursWorked, model.ContractHours),
                    ContractEarnings = contractEarnings = _payrollService.ContractHoursWorked(model.ContractHours, model.HoursWorked, model.HourlyRate),
                    OvertimeEarnings = overtimeEarnings = _payrollService.OvertimeEarnings(_payrollService.OvertimeRate(model.HourlyRate), overtimeHrs),
                    TotalEarnings = totalEarnings = _payrollService.TotalEarnings(overtimeEarnings, contractEarnings),
                    Tax = tax = _taxService.TaxAmount(totalEarnings),
                    SSN = ssn = _socialService.GetSocialDeductions(totalEarnings),
                    UnionFee = unionFees = _employeeService.UnionFees(model.EmpId),
                    StudentLoan = studentLoans = _employeeService.StudentLoans(model.EmpId, totalEarnings),
                    TotalDeduction = totalDeductions = _payrollService.TotalDeductions(tax, ssn, studentLoans,unionFees),
                    NetPayment = _payrollService.GetNetPay(totalEarnings, totalDeductions),
                };

                await _payrollService.CreatePaymentAsync(payment);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.employees = _employeeService.GetAllEmployeesForPayment();
            ViewBag.taxYears = _payrollService.GetTaxYearItems();
            return View();
        }
    }
}