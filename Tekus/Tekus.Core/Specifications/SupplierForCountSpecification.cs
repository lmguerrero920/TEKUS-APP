using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Core.Entities;

namespace Tekus.Core.Specifications
{
    public class SupplierForCountSpecification: BaseSpecification<Supplier>
    {
        public SupplierForCountSpecification(SupplierSpecificationParams supplierParams)
           : base(x =>
          (string.IsNullOrEmpty(supplierParams.Search) || x.Name.Contains(supplierParams.Search))
           &&(!supplierParams.ServicesId.HasValue || x.ServicesId == supplierParams.ServicesId))
        { }

    }
}
