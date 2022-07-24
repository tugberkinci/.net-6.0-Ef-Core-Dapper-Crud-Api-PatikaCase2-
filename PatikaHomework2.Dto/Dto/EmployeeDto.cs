using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PatikaHomework2.Dto.Dto
{
    public  class EmployeeDto
    {
      

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [ForeignKey("DepartmentId")]
        public int DepartmentId { get; set; }

        //nav
        //public ICollection<Department> Department { get; set; }
    }
}
