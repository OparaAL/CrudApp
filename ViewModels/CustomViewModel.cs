using CrudApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudApp.ViewModels
{
    public class CustomViewModel
    {
        public Employee Employee { get; set; }
        public List<Department> Departments { set; get; }
        public List<Employee> Employees { get; set; }
    }
}
