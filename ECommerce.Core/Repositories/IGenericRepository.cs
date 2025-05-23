using ECommerce.Core.model;
using ECommerce.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
       Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        void UpdateAsync(T entity, int id);
        void DeleteAsync(int id);

        void SaveAsync();


        Task<IReadOnlyList<T>> GetAllWithSpenAsync(ISpecification<T> spec);

        Task<T> GetByIdWithSpenAsync(ISpecification<T> spec);

        Task<int> GetCountWithSpecAsync(ISpecification<T> spec);

        Task AddAsync(T item);
    }
}
