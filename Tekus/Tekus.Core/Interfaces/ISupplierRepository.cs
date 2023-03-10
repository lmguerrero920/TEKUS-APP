using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Core.Entities;

namespace Tekus.Core.Interfaces
{
    public  interface ISupplierRepository
    {
        Task<Supplier> GetSupplierByIdAsync(int id);
        Task<IReadOnlyList<Supplier>>  GetSupplierAsync();

    }
}
