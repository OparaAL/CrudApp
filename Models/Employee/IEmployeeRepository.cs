using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudApp.Models
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAll();
        Employee GetById(int? id);
        void Create(Employee employee);
        void Delete(Employee employee);
        void Update(Employee employee);


    }
}
