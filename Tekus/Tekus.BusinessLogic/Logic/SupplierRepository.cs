using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.BusinessLogic.Data;
using Tekus.Core.Entities;
using Tekus.Core.Interfaces;

namespace Tekus.BusinessLogic.Logic
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly TekusDbContext _context;
        public SupplierRepository(TekusDbContext context)
        {        
            _context = context;
        }

        public  async Task<Supplier> GetSupplierByIdAsync(int id)
        {
            return await _context.Supplier.
                Include(p => p.Services).FirstOrDefaultAsync(
                p=>p.Id == id);
        }

        public async Task<IReadOnlyList<Supplier>> GetSupplierAsync()
        {
            return await _context.Supplier.Include(p=> p.Services).ToListAsync();
        }

        
    }
}
