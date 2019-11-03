using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudApp.Models
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private ApplicationDbContext repo;

        public DepartmentRepository(ApplicationDbContext repository)
        {
            repo = repository;
        }


        public void Create(Department department, bool check)
        {
            if (check)
            {
                foreach (var e in repo.Employees.ToList())
                {
                    if (e.RestoreDepartment == department.Code)
                    {
                        e.DepartmentCode = department.Code;
                    }
                    continue;
                }
            }
            repo.Departments.Add(department);
            repo.SaveChanges();
        }

        public void Delete(Department department)
        {
            repo.Departments.Remove(department);
            repo.SaveChanges();
        }

        public void DeleteAnyway(Department department)
        {
            foreach (var e in repo.Employees.ToList())
            {
                if (e.DepartmentCode == department.Code)
                {
                    e.DepartmentCode = null;
                }
                continue;
            }
            repo.Departments.Remove(department);
            repo.SaveChanges();
        }

        public List<Department> GetAll() => repo.Departments.ToList();


        public Department GetById(int? id)
        {
            Department department = repo.Departments.FirstOrDefault(c => c.Id == id);
            if (department != null) return department;
            else return null;
        }

        public void Update(Department dep, bool check)
        {
            Department department = repo.Departments.FirstOrDefault(d => d.Id == dep.Id);
            department.Name = dep.Name;
            department.Code = department.Code;
            if (check)
            {
                foreach (var e in repo.Employees.ToList())
                {
                    if (e.RestoreDepartment == department.Code)
                    {
                        e.DepartmentCode = department.Code;
                    }
                    continue;
                }
            }
            repo.Departments.Update(department);
            repo.SaveChanges();
        }
    }
}
