using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PaymentManagement.Entity;
using PaymentManagement.Services.Interface;
using PaymentManagement.Web.Models;

namespace PaymentManagement.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EmployeeController(IEmployeeService employeeService, IWebHostEnvironment webHost)
        {
            _employeeService = employeeService;
            _webHostEnvironment = webHost;
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

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee()
                {
                    Id = model.Id,
                    EmpNumber = model.EmpNumber,
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    Gender = model.Gender,
                    ImageUrl = model.ImageUrl.ToString(),
                    BirthDate = model.BirthDate,
                    CreateDate = model.DateJoined,
                    Role = model.Role,
                    EmailAddress = model.Email,
                    SSN = model.SSN,
                    Address = model.Address,
                    City = model.City,
                    PostalCode = model.PostalCode,
                    PaymentMethod = model.PaymentMethod,
                    StudentLoan = model.StudentLoan,
                    UnionMember = model.UnionMember,
                    PaymentRecords = model.PaymentRecords
                };

                if(model.ImageUrl.Length > 0 && model.ImageUrl != null)
                {
                    var imgPath = @"images/employee";
                    var fileName = Path.GetFileName(model.ImageUrl.FileName);
                    var ext = Path.GetFileNameWithoutExtension(model.ImageUrl.FileName);
                    var webRoot = _webHostEnvironment.WebRootPath;

                    fileName = DateTime.UtcNow.ToString("yyyy/mm/dd") + fileName + ext;
                    var path = Path.Combine(webRoot, imgPath, fileName);
                    await model.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));
                    employee.ImageUrl = "/" + imgPath + "/" + fileName;
                }              
                await _employeeService.CreateEmployeeAsync(employee);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _employeeService.GetById(id);
            if(employee == null)
            {
                return NotFound();
            }

            var model = new EditViewModel()
            {
                Id = employee.Id,
                EmpNumber = employee.EmpNumber,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Gender = employee.Gender,
                BirthDate = employee.BirthDate,
                DateJoined = employee.CreateDate,
                Role = employee.Role,
                Email = employee.EmailAddress,
                SSN = employee.SSN,
                Address = employee.Address,
                City = employee.City,
                PostalCode = employee.PostalCode,
                PaymentMethod = employee.PaymentMethod,
                StudentLoan = employee.StudentLoan,
                UnionMember = employee.UnionMember,
                PaymentRecords = employee.PaymentRecords
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee()
                {
                    Id = model.Id,
                    EmpNumber = model.EmpNumber,
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    Gender = model.Gender,
                    ImageUrl = model.ImageUrl.ToString(),
                    BirthDate = model.BirthDate,
                    CreateDate = model.DateJoined,
                    Role = model.Role,
                    EmailAddress = model.Email,
                    SSN = model.SSN,
                    Address = model.Address,
                    City = model.City,
                    PostalCode = model.PostalCode,
                    PaymentMethod = model.PaymentMethod,
                    StudentLoan = model.StudentLoan,
                    UnionMember = model.UnionMember,
                    PaymentRecords = model.PaymentRecords
                };

                if(model.ImageUrl != null && model.ImageUrl.Length > 0)
                {
                    var imgPath = @"images/employee";
                    var fileName = Path.GetFileName(model.ImageUrl.FileName);
                    var ext = Path.GetFileNameWithoutExtension(model.ImageUrl.FileName);
                    var webRoot = _webHostEnvironment.WebRootPath;

                    fileName = DateTime.UtcNow.ToString("yyyy/mm/dd") + fileName + ext;
                    var path = Path.Combine(webRoot, imgPath, fileName);
                    await model.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));
                    employee.ImageUrl = "/" + imgPath + "/" + fileName;
                }

                await _employeeService.UpdateEmployeeAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}