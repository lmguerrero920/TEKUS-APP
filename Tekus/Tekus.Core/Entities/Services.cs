using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Core.Entities
{
    public class Services: ClassBase
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Column(TypeName ="decimal(18,4)")]
        public decimal PriceHour { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }


    }
}
