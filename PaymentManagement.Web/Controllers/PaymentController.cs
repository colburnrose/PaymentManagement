using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaymentManagement.DataLayer.Repository;
using PaymentManagement.DataLayer.UnitOfWork;
using PaymentManagement.Entity;
using PaymentManagement.Services.Interface;
using PaymentManagement.Web.Models;
using RotativaCore;

namespace PaymentManagement.Web.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPayrollService _payrollService;
        private readonly IUnitOfWork _unitOfWork;
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

        public PaymentController(IPayrollService payrollService, IUnitOfWork unitOfWork, ITax taxService, ISocial socialService)
        {
            _payrollService = payrollService;
            _unitOfWork = unitOfWork;
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
            ViewBag.employees = _unitOfWork.EmployeeRepository.GetAllEmployeesForPayment();
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
                    FullName = _unitOfWork.EmployeeRepository.GetById(model.EmpId).FullName,
                    NINO = _unitOfWork.EmployeeRepository.GetById(model.EmpId).SSN,
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
                    UnionFee = unionFees = _unitOfWork.EmployeeRepository.UnionFees(model.EmpId),
                    StudentLoan = studentLoans = _unitOfWork.EmployeeRepository.StudentLoans(model.EmpId, totalEarnings),
                    TotalDeduction = totalDeductions = _payrollService.TotalDeductions(tax, ssn, studentLoans,unionFees),
                    NetPayment = _payrollService.GetNetPay(totalEarnings, totalDeductions),
                };

                await _payrollService.CreatePaymentAsync(payment);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.employees = _unitOfWork.EmployeeRepository.GetAllEmployeesForPayment();
            ViewBag.taxYears = _payrollService.GetTaxYearItems();
            return View();
        }

        [Route("api/Payment/Detail")]
        public IActionResult Detail(int id)
        {
            var payment = _payrollService.GetPaymentById(id);

            if(payment == null)
            {
                return NotFound();
            }

            var model = new PaymentDetailView
            {
                Id = payment.Id,
                EmpId = payment.EmpId,
                FullName = payment.FullName,
                NINO = payment.NINO,
                DatePaid = payment.DatePaid,
                PayMonth = payment.PayMonth,
                TaxYearId = payment.TaxYearId,
                Year = _payrollService.GetTaxYearById(payment.TaxYearId).YearOfTax,
                TaxCode = payment.TaxCode,
                HourlyRate = payment.HourlyRate,
                HoursWorked = payment.HoursWorked,
                ContractHours = payment.ContractHours,
                OvertimeHours = payment.OvertimeHours,
                OvertimeRate = _payrollService.OvertimeRate(payment.HourlyRate),
                ContractEarnings = payment.ContractEarnings,
                OvertimeEarnings = payment.OvertimeEarnings,
                Tax = payment.Tax,
                TaxYear = payment.TaxYear,
                SSN = payment.SSN,
                UnionFee = payment.UnionFee,
                StudentLoan = payment.StudentLoan,
                TotalEarnings = payment.TotalEarnings,
                TotalDeduction = payment.TotalDeduction,
                NetPayment = payment.NetPayment
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult PaySlip(int id)
        {
            var payment = _payrollService.GetPaymentById(id);

            if (payment == null)
            {
                return NotFound();
            }

            var model = new PaymentDetailView
            {
                Id = payment.Id,
                EmpId = payment.EmpId,
                FullName = payment.FullName,
                NINO = payment.NINO,
                DatePaid = payment.DatePaid,
                PayMonth = payment.PayMonth,
                TaxYearId = payment.TaxYearId,
                Year = _payrollService.GetTaxYearById(payment.TaxYearId).YearOfTax,
                TaxCode = payment.TaxCode,
                HourlyRate = payment.HourlyRate,
                HoursWorked = payment.HoursWorked,
                ContractHours = payment.ContractHours,
                OvertimeHours = payment.OvertimeHours,
                OvertimeRate = _payrollService.OvertimeRate(payment.HourlyRate),
                ContractEarnings = payment.ContractEarnings,
                OvertimeEarnings = payment.OvertimeEarnings,
                Tax = payment.Tax,
                TaxYear = payment.TaxYear,
                SSN = payment.SSN,
                UnionFee = payment.UnionFee,
                StudentLoan = payment.StudentLoan,
                TotalEarnings = payment.TotalEarnings,
                TotalDeduction = payment.TotalDeduction,
                NetPayment = payment.NetPayment
            };
            return View(model);
        }

        public IActionResult CreatePaySlipPdf(int id)
        {
            var payslip = new ActionAsPdf("PaySlip", new { id = id })
            {
                FileName = "payslip.pdf"
            };

            return payslip;
        }
    }
}