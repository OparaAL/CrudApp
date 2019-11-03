using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CrudApp.Models
{
    public class Department
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Input name of department")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Input code of department")]
        public string Code { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public Department()
        {
            Employees = new List<Employee>();
        }
    }
}
