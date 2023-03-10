using Tekus.Core.Entities;

namespace Tekus.WebAPI.DTOs
{
    public class ServicesDto
    {
        public string Name { get; set; }
        public decimal PriceHour { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }

    }
}
