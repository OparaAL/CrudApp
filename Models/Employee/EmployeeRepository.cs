using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudApp.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private ApplicationDbContext repo;

        public EmployeeRepository(ApplicationDbContext repository)
        {
            repo = repository;
        }

        public void Create(Employee employee)
        {
            employee.RestoreDepartment = employee.DepartmentCode;
            repo.Employees.Add(employee);
            repo.SaveChanges();
        }

        public void Delete(Employee employee)
        {
            repo.Employees.Remove(employee);
            repo.SaveChanges();
        }

        public List<Employee> GetAll() => repo.Employees.ToList();

        public Employee GetById(int? id)
        {
            Employee employee = repo.Employees.Include(c => c.Department).FirstOrDefault(e => e.Id == id);
            if (employee != null) return employee;
            else return null;
        }

        public void Update(Employee empl)
        {
            Employee employee = repo.Employees.FirstOrDefault(e => e.Id == empl.Id);
            employee.FullName = empl.FullName;
            employee.DateOfBirth = empl.DateOfBirth;
            employee.DepartmentCode = empl.DepartmentCode;
            employee.Salary = empl.Salary;
            employee.Code = employee.Code;
            employee.RestoreDepartment = employee.DepartmentCode;
            repo.Employees.Update(employee);
            repo.SaveChanges();
        }
    }
}
