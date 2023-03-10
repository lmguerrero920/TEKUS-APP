using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Core.Entities;
using Tekus.Core.Specifications;

namespace Tekus.Core.Interfaces
{
    public interface IGenericRepository<T> where T : ClassBase
    {
        Task<T> GetByIdAsync(int id);

        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> GetByIdWithSpec(ISpecification<T>spec);

        Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T>spec);
    }
}
