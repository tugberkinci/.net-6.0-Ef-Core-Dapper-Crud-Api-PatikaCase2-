using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatikaHomework2.Data.Model
{
    public class Folder
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }

        [MaxLength(5)]
        public string? AccessType { get; set; }

        //nav
        public ICollection<Employee> Employee { get; set; }
    }
}
