using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _context;

        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                var query = _context.Products
                    .OrderBy(p => p.Name) // Order by Id
                    .Include(p => p.productBrand)
                    .Include(p => p.productType);

                var products = trackChanges
                    ? await query.ToListAsync()
                    : await query.AsNoTracking().ToListAsync();

                return products.Cast<TEntity>();
            }

            var set = _context.Set<TEntity>();
            var list = trackChanges
                ? await set.ToListAsync()
                : await set.AsNoTracking().ToListAsync();

            return list;
        }


        // Implement the GetByIdAsync method to fulfill the interface contract
        public async Task<TEntity?> GetByIdAsync(TKey id)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                var product = await _context.Products
                    .Include(p => p.productBrand)
                    .Include(p => p.productType)
                    .FirstOrDefaultAsync(p => p.Id.Equals(id));

                return product as TEntity;
            }

            return await _context.Set<TEntity>().FindAsync(id);
        }


        // Implement AddAsync with return type Task<TEntity>
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();  // Save changes to the database
            return entity;  // Return the added entity
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();  // Save changes to the database
        }

        public void Delete(TKey id)
        {
            var entity = _context.Set<TEntity>().Find(id);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                _context.SaveChanges();  // Save changes to the database
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications, bool TrackChanges = false)
        {
          return await ApplySpecifications(specifications).ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await ApplySpecifications(specifications).FirstOrDefaultAsync();
        }
        public async Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await ApplySpecifications(specifications).CountAsync();
        }
        private IQueryable<TEntity> ApplySpecifications( ISpecifications<TEntity, TKey> specifications)
        {
            return SpecificationEvaluator.GetQuery(_context.Set<TEntity>(), specifications);
        }

      
    }
}
