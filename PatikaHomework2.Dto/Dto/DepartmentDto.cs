
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PatikaHomework2.Dto.Dto
{
    public class DepartmentDto
    {
       

        [MaxLength(10)]
        public string DeptName { get; set; }

        [ForeignKey("CountryId")]
        public int CountryId { get; set; }

        //nav

        //public ICollection<Country> Country { get; set; }
    }
}
