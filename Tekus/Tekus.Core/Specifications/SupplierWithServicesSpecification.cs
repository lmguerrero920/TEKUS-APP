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
        public SupplierWithServicesSpecification(SupplierSpecificationParams supplierParams)
            :base(x=>(!supplierParams.ServicesId.HasValue
            || x.ServicesId== supplierParams.ServicesId))
        {
            AddInclude(p => p.Services);
            AddInclude(p => p.Services.Country);
            // ApplyPaging(0,5);
            ApplyPaging(supplierParams.PageSize*(supplierParams.PageIndex-1),
                supplierParams.PageSize);
            if (!string.IsNullOrEmpty(supplierParams.Sort))
            {
                switch(supplierParams.Sort)
                {
                    case "IdAsc":
                        AddOrderBy(p => p.Id);
                        break;
                    case "IdDesc":
                        AddOrderByDescending(p=> p.Id);
                        break;
                    case "NameAsc":
                        AddOrderBy(p => p.Name);
                        break;
                    case "NameDesc":
                        AddOrderByDescending(p => p.Name);
                        break;
                    case "NITAsc":
                        AddOrderBy(p => p.NIT);
                        break;
                    case "NITDesc":
                        AddOrderByDescending(p => p.NIT);
                        break;

                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
        }
        public SupplierWithServicesSpecification(int id) :
            base(x => x.Id == id)
        {
            AddInclude(p => p.Services);
            AddInclude(p => p.Services.Country);
        } 
    }
}
