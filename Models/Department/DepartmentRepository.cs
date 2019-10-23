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


        public void Create(Department department)
        {
            repo.Departments.Add(department);
            repo.SaveChanges();
        }

        public void Delete(Department department)
        {
            foreach(var e in repo.Employees.ToList())
            {
                if(e.DepartmentId == department.Id)
                {
                    e.DepartmentId = null;
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

        public void Update(Department dep)
        {
            Department department = repo.Departments.FirstOrDefault(d => d.Id == dep.Id);
            department.Name = dep.Name;
            department.Code = dep.Code;
            repo.Departments.Update(department);
            repo.SaveChanges();
        }
    }
}
