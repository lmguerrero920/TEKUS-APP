using System.ComponentModel.DataAnnotations;
using Tekus.Core.Entities;

namespace Tekus.WebAPI.DTOs
{
    public class SupplierDto
    {
        public int Id { get; set; }
        public int NIT { get; set; } 
        public string Name { get; set; } 
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } 
        public int ServicesId { get; set; }
        //public string ServicesName { get; set; }
       public Services Services { get; set; }
    }
}
