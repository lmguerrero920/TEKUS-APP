using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Core.Entities;
using Tekus.Core.Interfaces;

namespace Tekus.BusinessLogic.Logic
{
    public class SupplierRepository : ISupplierRepository
    {
        public Task<Supplier> GetSupplierAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Supplier>> GetSupplierAsync()
        {
            throw new NotImplementedException();
        }
    }
}
