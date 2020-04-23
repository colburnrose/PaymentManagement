using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PaymentManagement.DataLayer.Repository;
using PaymentManagement.DataLayer.UnitOfWork;
using PaymentManagement.Entity;
using PaymentManagement.Services.Interface;
using PaymentManagement.Web.Models;

namespace PaymentManagement.Web.Controllers
{
    //[Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EmployeeController(IUnitOfWork unitOfWork, IWebHostEnvironment webHost)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHost;
        }

        [Route("api/Employee/Index")]
        public IActionResult Index(int? pageNumber)
        {
            var employees = _unitOfWork.EmployeeRepository.GetAllEmployees()
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
            int pageSize = 3;

            return View(EmployeeListPagination<EmployeeViewModel>.Create(employees, pageNumber ?? 1, pageSize));
        }

        [HttpGet]
        [Route("api/Employee/Create")]
        public IActionResult Create()
        {
            var model = new CreateViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/Employee/Create")]
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
                    FullName = model.FullName,
                    Gender = model.Gender,
                    ImageUrl = model.ImageUrl.ToString(),
                    BirthDate = model.BirthDate,
                    CreateDate = model.DateJoined,
                    Role = model.Role,
                    EmailAddress = model.Email,
                    Phone = model.PhoneNumber,
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
                    var fileName = Path.GetFileNameWithoutExtension(model.ImageUrl.FileName);
                    var ext = Path.GetExtension(model.ImageUrl.FileName);             
                    var webRoot = _webHostEnvironment.WebRootPath;

                    fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + ext;
                    var path = Path.Combine(webRoot, imgPath, fileName);
                    await model.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));
                    employee.ImageUrl = "/" + imgPath + "/" + fileName;
                }              
                await _unitOfWork.EmployeeRepository.CreateEmployeeAsync(employee);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]
        [Route("api/Employee/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
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
                PhoneNumber = employee.Phone,
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
        [ValidateAntiForgeryToken]
        [Route("api/Employee/Edit/{id}")]
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
                    BirthDate = model.BirthDate,
                    CreateDate = model.DateJoined,
                    Role = model.Role,
                    EmailAddress = model.Email,
                    Phone = model.PhoneNumber,
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
                    var fileName = Path.GetFileNameWithoutExtension(model.ImageUrl.FileName);
                    var ext = Path.GetExtension(model.ImageUrl.FileName);
                    var webRoot = _webHostEnvironment.WebRootPath;

                    fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + ext;
                    var path = Path.Combine(webRoot, imgPath, fileName);
                    await model.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));
                    employee.ImageUrl = "/" + imgPath + "/" + fileName;
                }

                await _unitOfWork.EmployeeRepository.UpdateEmployeeAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [Route("api/Employee/Detail/{id}")]
        public IActionResult Detail(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);

            if(employee == null)
            {
                return NotFound();
            }

            var model = new DetailViewModel()
            {
                Id = employee.Id,
                EmpNumber = employee.EmpNumber,
                FullName = employee.FullName,
                Gender = employee.Gender,
                ImageUrl = employee.ImageUrl,
                BirthDate = employee.BirthDate,
                CreateDate = employee.CreateDate,
                Role = employee.Role,
                EmailAddress = employee.EmailAddress,
                SSN = employee.SSN,
                Phone = employee.Phone,
                PaymentMethod = employee.PaymentMethod,
                StudentLoan = employee.StudentLoan,
                UnionMember = employee.UnionMember,
                Address = employee.Address,
                City = employee.City,
                PostalCode = employee.PostalCode
            };

            return View(model);
        }

        [HttpGet]
        [Route("api/Employee/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            if(employee == null)
            { 
                return NotFound();
            }

            var model = new DeleteViewModel()
            {
                Id = employee.Id,
                FullName = employee.FullName,
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/Employee/Delete/{id}")]
        public async Task<IActionResult> Delete(DeleteViewModel model)
        {
            await _unitOfWork.EmployeeRepository.DeleteEmployeeById(model.Id);
            return RedirectToAction(nameof(Index));
        }


    }
}