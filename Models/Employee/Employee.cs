﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CrudApp.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Input fullname of employee")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Input date of birth")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Input code")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Input fullname of employee")]
        public int Salary { get; set; }

        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
