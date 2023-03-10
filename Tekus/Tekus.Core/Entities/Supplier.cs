using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Core.Entities
{
    public class Supplier : ClassBase
    {
      
        public int NIT { get; set; } 
        public string Name { get; set; } 
        [DataType(DataType.EmailAddress)] 
        public string Email { get; set; } 
        public int ServicesId { get; set; }
        public Services Services { get; set; }

    }
}
