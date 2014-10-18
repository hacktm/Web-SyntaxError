using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using CollegeBuffer.DAL.Model;

namespace CollegeBuffer.DAL.Context
{
    public abstract class BaseRepository<T> where T : AbstractModel
    {
        private readonly DatabaseContext _dbContext;
        private readonly DbSet<T> _dbSet;

        protected BaseRepository(DatabaseContext context)
        {
            _dbContext = context;
            _dbSet = context.Set<T>();
        }

        #region Persistence logic

        public bool Save()
        {
            try
            {
                _dbContext.SaveChanges();

                return true;
            }

            catch
            {
                return false;
            }
        }

        #endregion

        #region CRUD

        public T Get(Guid id)
        {
            var entity = _dbSet.Find(id);
            return entity;
        }

        public T[] GetAll()
        {
            var entities = _dbSet.ToArray();
            return entities;
        }

        public T Insert(T entity)
        {
            if (entity.Id == Guid.Empty)
                entity.Id = Guid.NewGuid();

            entity = _dbSet.Add(entity);

            return Save() ? entity : null;
        }

        public T Update(T entity)
        {
            _dbSet.AddOrUpdate(entity);

            return Save() ? Get(entity.Id) : null;
        }

        public bool Delete(T entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);

            _dbSet.Remove(entity);

            return Save();
        }

        public bool Delete(Guid id)
        {
            var entity = _dbSet.Find(id);

            return Delete(entity);
        }

        #endregion
    }
}