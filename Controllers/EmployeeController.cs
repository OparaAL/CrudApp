using CrudApp.Models;
using CrudApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public IActionResult Index(string searchString, int dep)
        {
            ViewData["CurrentFilter"] = searchString;
            List<Department> deps = deptManager.GetAll();
            deps.Insert(0, new Department{ Id = 0, Name = "All Departments", Code = null});
            CustomViewModel cmView = new CustomViewModel
            {
                Employees = empManager.GetAll(),
                Departments = deps
            };
            if (!String.IsNullOrEmpty(searchString) && dep != 0)
            {
                cmView.Employees = cmView.Employees.Where(e => e.FullName.Contains(searchString)
                && e.DepartmentId == dep).ToList();
            }
            else if (String.IsNullOrEmpty(searchString) && dep != 0)
            {
                cmView.Employees = cmView.Employees.Where(e => e.DepartmentId == dep).ToList();
            }
            else if (dep == 0 && !String.IsNullOrEmpty(searchString))
            {
                cmView.Employees = cmView.Employees.Where(e => e.FullName == searchString).ToList();
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
            var cmView = new CustomViewModel
            {
                Departments = deptManager.GetAll()
            };
            if (ModelState.IsValid)
            {
                empManager.Create(employee);
                return RedirectToAction("Index");
            }
            else return View(cmView);
            
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
