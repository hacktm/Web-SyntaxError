using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using CollegeBuffer.DAL.Model.Abstract;

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

        public int Save()
        {
            return DbContext.SaveChanges();
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

            var result = Save() != 0 ? entity : null;
            return result;
        }

        public T Update(T entity)
        {
            DbSet.AddOrUpdate(entity);

            return Save() != 0 ? Get(entity.Id) : null;
        }

        public bool Delete(T entity)
        {
            if (DbContext.Entry(entity).State == EntityState.Detached)
                DbSet.Attach(entity);

            DbSet.Remove(entity);

            return Save() != 0;
        }

        public bool Delete(Guid id)
        {
            var entity = DbSet.Find(id);

            return Delete(entity);
        }

        #endregion
    }
}