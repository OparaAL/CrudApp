using CrudApp.Models;
using CrudApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CrudApp.Controllers
{
    public class EmployeeController : Controller
    {
        private ApplicationDbContext context;
        private IEmployeeRepository empManager;
        private IDepartmentRepository deptManager;
        public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository,
            ApplicationDbContext cont)
        {
            context = cont;
            empManager = employeeRepository;
            deptManager = departmentRepository;
        }

        public IActionResult Index(string searchString, string dep)
        {
            ViewData["CurrentFilter"] = searchString;
            List<Department> deps = deptManager.GetAll();
            deps.Insert(0, new Department{ Id = 0, Name = "All Departments", Code = null});
            CustomViewModel cmView = new CustomViewModel
            {
                Employees = empManager.GetAll(),
                Departments = deps
            };
            if (!String.IsNullOrEmpty(searchString) && dep != null)
            {
                cmView.Employees = cmView.Employees.Where(e => (e.FullName.Contains(searchString)
                || e.Code.Contains(searchString))
                && e.DepartmentCode == dep).ToList();
            }
            else if (String.IsNullOrEmpty(searchString) && dep != null)
            {
                cmView.Employees = cmView.Employees.Where(e => e.DepartmentCode == dep).ToList();
            }
            else if (dep == null && !String.IsNullOrEmpty(searchString))
            {
                cmView.Employees = cmView.Employees.Where(e => e.FullName.Contains(searchString)
                || e.Code.Contains(searchString)).ToList();
            }
            else cmView.Employees = cmView.Employees.ToList();
            return View(cmView);
        }

        public IActionResult Create()
        {
            var cmView = new CustomViewModel
            {
                Departments = deptManager.GetAll()
            };
            return View(cmView);
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            ViewBag.Error = null;
            var cmView = new CustomViewModel
            {
                Departments = deptManager.GetAll()
            };

            try
            {
                if (ModelState.IsValid)
                {
                    empManager.Create(employee);
                    return RedirectToAction("Index");
                }
                else return View(cmView);
            }
            catch
            {
                ViewBag.Error = "Сотрудник с таким кодом уже существует";
                return View(cmView);
            }

        }

        public IActionResult Edit(int? id)
        {
            var cmView = new CustomViewModel
            {
                Employee = empManager.GetById(id),
                Departments = deptManager.GetAll()
            };
            return View(cmView);
        }

        [HttpPost]
        public IActionResult Edit(Employee employee, int? id)
        {
            var cmView = new CustomViewModel
            {
                Employee = empManager.GetById(id),
                Departments = deptManager.GetAll()
            };

                if (ModelState.IsValid)
                {
                    empManager.Update(employee);
                    return RedirectToAction("Index");
                }
                else return View(cmView);
        }


        public IActionResult Details(int? id)
        {
            if (id != null)
            {
                Employee employee = empManager.GetById(id);
                if (employee != null) return View(employee);
            }
            return NotFound();
        }



        [HttpGet]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Employee employee = empManager.GetById(id);
                if (employee != null)
                    return View(employee);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                Employee employee = empManager.GetById(id);
                empManager.Delete(employee);
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
