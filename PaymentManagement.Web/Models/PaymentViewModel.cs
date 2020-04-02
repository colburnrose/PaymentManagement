using PaymentManagement.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentManagement.Web.Models
{
    public class PaymentViewModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [Display(Name = "Name")]
        public string FullName { get; set; }
        [Display(Name = "Pay Date")]
        public DateTime PayDate { get; set; }
        [Display(Name = "Month")]
        public string PayMonth { get; set; }
        public int TaxYearId { get; set; }
        public string Year { get; set; }
        [Display(Name = "Total Earnings")]
        public decimal TotalEarnings { get; set; }
        [Display(Name = "Total Deductions")]
        public decimal TotalDeductions { get; set; }
        [Display(Name = "Net Payment")]
        public decimal NetPayment { get; set; }

    }

    public class PaymentCreateModel
    {
        public int Id { get; set; }
        [Display(Name = "Full Name")]
        public int EmpId { get; set; }
        public Employee Employee { get; set; }
        public string FullName { get; set; }
        public string NINO { get; set; }
        [DataType(DataType.Date), Display(Name = "Pay Date")]
        public DateTime DatePaid { get; set; } = DateTime.UtcNow;
        [Display(Name = "Month Paid")]
        public string PayMonth { get; set; } = DateTime.Today.Month.ToString();
        [Display(Name = "Tax Year")]
        public int TaxYearId { get; set; }
        public TaxYear TaxYear { get; set; }
        public string TaxCode { get; set; } = "1250L"; // default tax code
        [Display(Name = "Hourly Rate")]
        public decimal HourlyRate { get; set; }
        [Display(Name = "Hours Worked")]
        public decimal HoursWorked { get; set; }
        [Display(Name = "Contract Hours")]
        public decimal ContractHours { get; set; }
        [Display(Name = "Overtime Hours")]
        public decimal OvertimeHours { get; set; }
        [Display(Name = "Contract Earnings")]
        public decimal ContractEarnings { get; set; }
        [Display(Name = "Overtime Earnings")]
        public decimal OvertimeEarnings { get; set; }
        [Display(Name = "Tax")]
        public decimal Tax { get; set; }
        [Display(Name = "SSN")]
        public decimal SSN { get; set; }
        [Display(Name = "Union Fee")]
        public decimal? UnionFee { get; set; }
        [Display(Name = "Student Loan")]
        public decimal? StudentLoan { get; set; }
        [Display(Name = "Total Earnings")]
        public decimal TotalEarnings { get; set; }
        [Display(Name = "Total Deduction")]
        public decimal TotalDeduction { get; set; }
        [Display(Name = "Net Pay")]
        public decimal NetPayment { get; set; }
    }

    public class PaymentDetailView
    {
        public int Id { get; set; }
        [Display(Name = "Employee")]
        public int EmpId { get; set; }
        public Employee Employee { get; set; }
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        public string NINO { get; set; }
        [DataType(DataType.Date), Display(Name = "Pay Date")]
        public DateTime DatePaid { get; set; }
        [Display(Name = "Month Paid")]
        public string PayMonth { get; set; }
        [Display(Name = "Tax Year")]
        public int TaxYearId { get; set; }
        public string Year { get; set; }
        public TaxYear TaxYear { get; set; }
        [Display(Name = "Tax Code")]
        public string TaxCode { get; set; } // default tax code
        [Display(Name = "Hourly Rate")]
        public decimal HourlyRate { get; set; }
        [Display(Name = "Hours")]
        public decimal HoursWorked { get; set; }
        [Display(Name = "Contract Hours")]
        public decimal ContractHours { get; set; }
        [Display(Name = "Overtime Hours")]
        public decimal OvertimeHours { get; set; }
        [Display(Name = "Overtime Rate")]
        public decimal OvertimeRate { get; set; }
        [Display(Name = "Contract Earnings")]
        public decimal ContractEarnings { get; set; }
        [Display(Name = "Overtime Earnings")]
        public decimal OvertimeEarnings { get; set; }
        [Display(Name = "Tax")]
        public decimal Tax { get; set; }
        [Display(Name = "SSN")]
        public decimal SSN { get; set; }
        [Display(Name = "Union Fee")]
        public decimal? UnionFee { get; set; }
        [Display(Name = "Student Loan")]
        public decimal? StudentLoan { get; set; }
        [Display(Name = "Total Earnings")]
        public decimal TotalEarnings { get; set; }
        [Display(Name = "Total Deductions")]
        public decimal TotalDeduction { get; set; }
        [Display(Name = "Net Pay")]
        public decimal NetPayment { get; set; }
    }
}
