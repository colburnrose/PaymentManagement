using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaymentManagement.Services.Interface;
using PaymentManagement.Web.Models;

namespace PaymentManagement.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public IActionResult Index()
        {
            var employees = _employeeService.GetAllEmployees()
                .Select(emp => new EmployeeViewModel 
                {
                    Id = emp.Id,
                    EmpNumber = emp.EmpNumber,
                    FullName = emp.FullName,
                    Gender = emp.Gender,
                    ImageUrl = emp.ImageUrl,
                    DateJoined = emp.CreateDate,
                    Role = emp.Role,
                    City = emp.City
                }).ToList();
            return View(employees);
        }
    }
}