using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatikaHomework2.Dto.Dto
{
    public class CountryDto
    {
    

        [MaxLength(30)]
        public string CountryName { get; set; }

        [MaxLength(10)]
        public string Continent { get; set; }

        [MaxLength(3)]
        public string Currency { get; set; }
    }
}
