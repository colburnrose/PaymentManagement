using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaymentManagement.Entity
{
    public class PaymentRecord
    {
        public int Id { get; set; }
        [ForeignKey("Employee")]
        public int EmpId { get; set; }
        public Employee Employee { get; set; }
        [MaxLength(50)]
        public string FullName { get; set; }
        public string NINO { get; set; }
        public DateTime DatePaid { get; set; }
        public string PayMonth { get; set; }
        [ForeignKey("TaxYear")]
        public int TaxYearId { get; set; }
        public TaxYear TaxYear { get; set; }
        public string TaxCode { get; set; }
        [Column(TypeName = "money")]
        public decimal HourlyRate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal HoursWorked { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ContractHours { get; set; } = 40m;
        [Column(TypeName = "decimal(18,2)")]
        public decimal OvertimeHours { get; set; }
        [Column(TypeName = "money")]
        public decimal ContractEarnings { get; set; }
        [Column(TypeName = "money")]
        public decimal OvertimeEarnings { get; set; }
        [Column(TypeName = "money")]
        public decimal Tax { get; set; }
        [Column(TypeName = "money")]
        public decimal SSN { get; set; }
        [Column(TypeName = "money")]
        public decimal? UnionFee { get; set; }
        [Column(TypeName = "money")]
        public decimal? StudentLoan { get; set; }
        [Column(TypeName = "money")]
        public decimal TotalEarnings { get; set; }
        [Column(TypeName = "money")]
        public decimal TotalDeduction { get; set; }
        [Column(TypeName ="money")]
        public decimal NetPayment { get; set; }
    }
}
