using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using CollegeBuffer.DAL.Model;

namespace CollegeBuffer.DAL.Context
{
    public abstract class BaseRepository<T> where T : AbstractModel
    {
        protected readonly DatabaseContext DbContext;
        protected readonly DbSet<T> DbSet;

        protected BaseRepository(DatabaseContext context)
        {
            DbContext = context;
            DbSet = context.Set<T>();
        }

        #region Persistence logic

        public bool Save()
        {
            try
            {
                DbContext.SaveChanges();

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
            var entity = DbSet.Find(id);
            return entity;
        }

        public T[] GetAll()
        {
            var entities = DbSet.ToArray();
            return entities;
        }

        public T Insert(T entity)
        {
            if (entity.Id == Guid.Empty)
                entity.Id = Guid.NewGuid();

            entity = DbSet.Add(entity);

            return Save() ? entity : null;
        }

        public T Update(T entity)
        {
            DbSet.AddOrUpdate(entity);

            return Save() ? Get(entity.Id) : null;
        }

        public bool Delete(T entity)
        {
            if (DbContext.Entry(entity).State == EntityState.Detached)
                DbSet.Attach(entity);

            DbSet.Remove(entity);

            return Save();
        }

        public bool Delete(Guid id)
        {
            var entity = DbSet.Find(id);

            return Delete(entity);
        }

        #endregion
    }
}