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
        
        public string Name { get; set; }
         
        public decimal PriceHour { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }


    }
}
