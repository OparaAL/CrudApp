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
                departments = departments.Where(d => d.Name.Contains(searchString)
                || d.Code.Contains(searchString)).ToList();
            }
            return View(departments);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department, bool check)
        {
            try {
                ViewBag.Error = null;
            if (ModelState.IsValid)
            {
                deptManager.Create(department, check);
                return RedirectToAction("Index");
            }
            else return View();
            }
            catch
            {
                ViewBag.Error = "Отдел с таким кодом или именем уже существует";
                return View();
            }
        }

        public IActionResult Edit(int? id)
        {
            ViewBag.Error = null;
            if (id != null)
            {
                Department department = deptManager.GetById(id);
                return View(department); ;
            }
            else return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Department department, bool check)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    deptManager.Update(department, check);
                    return RedirectToAction("Index");
                }
                else return View(department);
            }
            catch
            {
                ViewBag.ErrorEdit = "Департамент с таким названием уже существует";
                return View(department);
            }
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
            Department department = deptManager.GetById(id);
            try
            {
                if (id != null)
                {
                    ViewBag.ErrorDelete = null;
                    if (department != null)
                    {
                        deptManager.Delete(department);
                        return RedirectToAction("Index");
                    }
                }
                return NotFound();
            }
            catch
            {
                return RedirectToAction("DeleteAnyway", new {id = department.Id });
            }
        }

        [HttpGet]
        [ActionName("DeleteAnyway")]
        public IActionResult DeleteAnywayConfirm(int? id)
        {
            ViewBag.ErrorDelete = "You are trying to delete department with employees. All employee department fields will be set to null. Are you sure ?";
            Department department = deptManager.GetById(id);
            if (department != null)
            {
                return View(department);
            }
            else return NotFound();
        }

        [HttpPost]
        public IActionResult DeleteAnyway(int? id)
        {
            if(id != null)
            {
                Department department = deptManager.GetById(id);
                {
                    if (department != null)
                    {
                        deptManager.DeleteAnyway(department);
                        return RedirectToAction("Index");
                    }
                }
            }
            return NotFound();
        }
    }
}
