using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatikaHomework2.Dto.Dto
{
    public class FolderDto
    {
      

        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }

        [MaxLength(5)]
        public string? AccessType { get; set; }

        //nav
        //public ICollection<Employee> Employee { get; set; }
    }
}
