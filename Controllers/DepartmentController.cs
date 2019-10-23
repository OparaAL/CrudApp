using CrudApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudApp.Controllers
{
    public class DepartmentController : Controller
    {
        private IDepartmentRepository deptManager;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            deptManager = departmentRepository;
        }

        public IActionResult Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var departments = deptManager.GetAll();
            if (!String.IsNullOrEmpty(searchString))
            {
                departments = departments.Where(e => e.Name.Contains(searchString)).ToList();
            }
            return View(departments);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                deptManager.Create(department);
                return RedirectToAction("Index");
            }
            else return View();
        }

        public IActionResult Edit(int? id)
        {
            if(id != null)
            {
                Department department = deptManager.GetById(id);
                return View(department); ;
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Department department, int? id)
        {
            if (ModelState.IsValid)
            {
                deptManager.Update(department);
                return RedirectToAction("Index");
            }
            else return View(department);
        }

        public IActionResult Details(int? id)
        {
            if (id != null)
            {
                Department department = deptManager.GetById(id);
                if (department != null) return View(department);
            }
            return NotFound();
        }

        [HttpGet]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Department department = deptManager.GetById(id);
                if (department != null)
                    return View(department);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                Department department = deptManager.GetById(id);
                if (department != null)
                {
                    deptManager.Delete(department);
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }

    }
}
