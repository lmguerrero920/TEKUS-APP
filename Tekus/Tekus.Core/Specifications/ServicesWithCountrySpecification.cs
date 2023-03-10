using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Core.Entities;

namespace Tekus.Core.Specifications
{
    public class ServicesWithCountrySpecification: BaseSpecification<Services>
    {
        public ServicesWithCountrySpecification()
        {
            AddInclude(p => p.Country); 
        }
        public ServicesWithCountrySpecification(int id) :
            base(x => x.Id == id)
        {
            AddInclude(p => p.Country);
        }
    }
}
