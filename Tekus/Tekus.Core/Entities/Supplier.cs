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
     //   public int SupplierId { get; set; }

        //[Required]
        //[MaxLength(15)]
        public int NIT { get; set; }
       // [Required]

      //  [MaxLength(100)]

        public string Name { get; set; }


        //[Required]
        [DataType(DataType.EmailAddress)]
      //  [Display(Name = "Correo Electronico")]
      //  [EmailAddress(ErrorMessage = "Invalid email address")]
      //  [MaxLength(100)]
        public string Email { get; set; }

        public int ServicesId { get; set; }
        public Services Services { get; set; }

    }
}
