using Microsoft.EntityFrameworkCore;
using Sign.Db.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sign.Db.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly SignContext entities;

        protected BaseRepository(IDbContextFactory<SignContext> signContextFactory)
        {
            entities = signContextFactory.CreateDbContext();
        }

        public virtual async Task<T> GetAsync(Guid id)
        {
            T entity = await entities.Set<T>().FindAsync(id);
            return entity;
        }

        public virtual async Task<IList<T>> GetAllAsync()
        {
            IList<T> query = await entities.Set<T>().ToListAsync();
            return query;
        }

        public async Task<IList<T>> FindByAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            IList<T> query = await entities.Set<T>().Where(predicate).ToListAsync();
            return query;
        }
        /// <summary>
        /// Adds a new rocord to entities. Save must be called to update database
        /// </summary>
        /// <param name="entity"></param>
        public virtual async Task AddAsync(T entity)
        {
            await entities.Set<T>().AddAsync(entity);
        }

        /// <summary>
        /// Deletes a record from entities.  Save must be called to update database
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
        {
            entities.Set<T>().Remove(entity);
        }
        /// <summary>
        /// Updates and saves db 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Returns number of records affected by update</returns>
        public async Task<int> UpdateAsync(T entity)
        {
            entities.Entry(entity).State = EntityState.Modified;
            entities.Update(entity);
            return await entities.SaveChangesAsync();
        }

        /// <summary>
        /// Saves the current state of entities
        /// </summary>
        /// <returns>Returns number of records affected by update</returns>
        public virtual async Task<int> SaveAsync()
        {
            return await entities.SaveChangesAsync();
        }
    }
}
