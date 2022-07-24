using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatikaHomework2.Data.Model
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(10)]
        public string DeptName { get; set; }

        [ForeignKey("CountryId")]
        public int CountryId { get; set; }

        //nav

        public ICollection<Country> Country { get; set; }
    }
}
