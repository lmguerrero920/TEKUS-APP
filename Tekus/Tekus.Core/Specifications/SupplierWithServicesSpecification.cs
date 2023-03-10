using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Core.Entities;

namespace Tekus.Core.Specifications
{
    public class SupplierWithServicesSpecification : BaseSpecification<Supplier>
    {
        public SupplierWithServicesSpecification()
        {
            AddInclude(p => p.Services); 
        }
        public SupplierWithServicesSpecification(int id) :
            base(x => x.Id == id)
        {
            AddInclude(p => p.Services);
        } 
    }
}
