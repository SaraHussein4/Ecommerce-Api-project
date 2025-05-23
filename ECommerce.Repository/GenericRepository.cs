using ECommerce.Core.model;
using ECommerce.Core.Repositories;
using ECommerce.Core.Specifications;
using ECommerce.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        public StoreContext StoreContext { get; set; }

        public GenericRepository(StoreContext storeContext)
        {
            StoreContext = storeContext;
        }

      
        public async Task<IReadOnlyList<T>> GetAllAsync()

        {
            if (typeof(T) == typeof(Product))
                return (IReadOnlyList<T>)await StoreContext.Products.Include(p => p.ProductBrand).Include(p => p.ProductType).ToListAsync();
            return await StoreContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return  await StoreContext.Set<T>().FindAsync(id);
        }

        public async void Delete(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                StoreContext.Set<T>().Remove(entity);
            }
        }
        public void SaveAsync()
        {
            StoreContext.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            StoreContext.Set<T>().Update(entity);
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpenAsync(ISpecification<T> spec)
        {
            return await ApplyingSpecification(spec).ToListAsync();
        }

        public async Task<T> GetByIdWithSpenAsync(ISpecification<T> spec)
        {
            return await ApplyingSpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<int> GetCountWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplyingSpecification(spec).CountAsync();
        }
        private IQueryable<T> ApplyingSpecification(ISpecification<T> spec)
        {
            return SpecificationEvalutor<T>.GetQuery(StoreContext.Set<T>(), spec);
        }

        public async Task AddAsync(T item)
        {
            await StoreContext.Set<T>().AddAsync(item);
        }

        public void UpdateAsync(T entity, int id)
        {
            var existing = StoreContext.Set<T>().Find(id);
            if (existing == null)
                throw new ArgumentException($"Entity with id {id} not found");

            var entry = StoreContext.Entry(existing);
            var newValues = StoreContext.Entry(entity).CurrentValues;

            foreach (var property in entry.Properties)
            {
                if (property.Metadata.IsPrimaryKey())
                    continue;

                property.CurrentValue = newValues[property.Metadata.Name];
            }
        }

        public  void DeleteAsync(int id)
        {
            var entity = StoreContext.Set<T>().Find(id);
            if (entity == null)
                throw new ArgumentException($"Entity with id {id} not found");

            StoreContext.Set<T>().Remove(entity);
        }
    }
}
