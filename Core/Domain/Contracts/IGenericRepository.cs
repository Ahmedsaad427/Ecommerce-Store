using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool TrackChanges = false);
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity,TKey> specifications,bool TrackChanges = false);

        Task<TEntity?> GetByIdAsync(TKey id);
        Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications);

        Task<TEntity> AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TKey id);

    }
}
