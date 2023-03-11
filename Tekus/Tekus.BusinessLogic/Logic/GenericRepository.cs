using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.BusinessLogic.Data;
using Tekus.Core.Entities;
using Tekus.Core.Interfaces;
using Tekus.Core.Specifications;

namespace Tekus.BusinessLogic.Logic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ClassBase
    {

        private readonly TekusDbContext _context;
        public GenericRepository(TekusDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
          return await  _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }


        public async Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec)
        {
          return await  ApplySpecification(spec).ToListAsync();
        }

        public async Task<T> GetByIdWithSpec(ISpecification<T> spec)
        {
           return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(
                _context.Set<T>().AsQueryable(),spec);
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<int> Add(T enty)
        {
           _context.Set<T>().Add(enty);
          return  await _context.SaveChangesAsync();
        }

        public  async Task<int> Update(T enty)
        {
            _context.Set<T>().Attach(enty);
            _context.Entry(enty).State= EntityState.Modified;
           return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        { 
            _context.Entry(id).State = EntityState.Deleted;
            return await _context.SaveChangesAsync();
        }

      
    }
}
