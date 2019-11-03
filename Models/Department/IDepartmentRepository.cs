using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudApp.Models
{
    public interface IDepartmentRepository
    {
        void Create(Department department, bool check);
        void Update(Department department, bool check);
        void Delete(Department department);
        void DeleteAnyway(Department department);
        List<Department> GetAll();
        Department GetById(int? id);
    }
}
