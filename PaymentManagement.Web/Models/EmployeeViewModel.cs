using System;
using System.Collections.Generic;
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
}
