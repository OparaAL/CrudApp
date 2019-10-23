using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudApp.Models
{
    public interface IDepartmentRepository
    {
        void Create(Department department);
        void Update(Department department);
        void Delete(Department department);
        List<Department> GetAll();
        Department GetById(int? id);
    }
}
