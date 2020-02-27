using Microsoft.AspNetCore.Http;
using PaymentManagement.Entity;
using PaymentManagement.Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentManagement.Web.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string EmpNumber { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateJoined { get; set; }
        public string Role { get; set; }
        public string City { get; set; }
    }

    public class CreateViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Employee number is required."), RegularExpression(@"^[A-Z]{3,3}[0-9]{3}$")]
        public string EmpNumber { get; set; }
        [Required(ErrorMessage = "First name is required."),StringLength(50, MinimumLength = 2), Display(Name = "First Name")]
        [RegularExpression(@"^[A-Z][a-zA-Z""'\s-]*$")]
        public string FirstName { get; set; }
        [StringLength(50), Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [Required(ErrorMessage ="Last name is required."),StringLength(50, MinimumLength = 2), RegularExpression(@"^[A-Z][a-zA-Z""'\s-]*$"), Display(Name="Last Name")]
        public string LastName { get; set; }
        public string FullName => FirstName + (string.IsNullOrEmpty(MiddleName) ? " " : (" " + (char?)MiddleName[0] + ".").ToUpper()) + LastName;
        public string Gender { get; set; }
        [Display(Name = "Photo")]
        public IFormFile ImageUrl { get; set; }
        [DataType(DataType.Date), Display(Name= "Birth Date")]
        public DateTime BirthDate { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Joined")]
        public DateTime DateJoined { get; set; }
        [Required(ErrorMessage ="Role is required."), StringLength(100)]
        public string Role { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string SSN { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required, StringLength(10), Display(Name= "Postal Code")]
        public string PostalCode { get; set; }
        [Display(Name = "Payment Method")]
        public PaymentMethod PaymentMethod { get; set; }
        [Display(Name = "Student Loan")]
        public StudentLoan StudentLoan { get; set; }
        [Display(Name = "Union Member")]
        public UnionMember UnionMember { get; set; }
        public IEnumerable<PaymentRecord> PaymentRecords { get; set; }
    }

    public class EditViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Employee number is required."), RegularExpression(@"^[A-Z]{3,3}[0-9]{3}$")]
        public string EmpNumber { get; set; }
        [Required(ErrorMessage = "First name is required."), StringLength(50, MinimumLength = 2), RegularExpression(@"^[A-Z][a-zA-Z""'\s-]*$"), Display(Name = "First Name")]
        public string FirstName { get; set; }
        [StringLength(50), Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Last name is required."), StringLength(50, MinimumLength = 2), RegularExpression(@"^[A-Z][a-zA-Z""'\s-]*$"), Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string FullName => FirstName + (string.IsNullOrEmpty(MiddleName) ? " " : MiddleName) + LastName;
        public string Gender { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Photo")]
        public IFormFile ImageUrl { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateJoined { get; set; }
        [Required(ErrorMessage = "Role is required."), StringLength(100)]
        public string Role { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string SSN { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        [Required, StringLength(10), Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        [Display(Name = "Payment Method")]
        public PaymentMethod PaymentMethod { get; set; }
        [Display(Name = "Student Loan")]
        public StudentLoan StudentLoan { get; set; }
        [Display(Name = "Union Member")]
        public UnionMember UnionMember { get; set; }
        public IEnumerable<PaymentRecord> PaymentRecords { get; set; }
    }

    public class DetailViewModel
    {
        public int Id { get; set; }
        public string EmpNumber { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string ImageUrl { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreateDate { get; set; }
        public string Role { get; set; }
        public string EmailAddress { get; set; }
        public string SSN { get; set; }
        public string Phone { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public StudentLoan StudentLoan { get; set; }
        public UnionMember UnionMember { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }

    public class DeleteViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }
}
