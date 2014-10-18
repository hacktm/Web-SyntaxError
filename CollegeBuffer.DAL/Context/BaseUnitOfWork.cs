using System;

namespace CollegeBuffer.DAL.Context
{
    public class BaseUnitOfWork : IDisposable
    {
        public DatabaseContext DbContext { get; private set; }

        public BaseUnitOfWork()
        {
            _disposed = false;
            DbContext = new DatabaseContext();
        }

        public int SaveAllChanges()
        {
            return DbContext.SaveChanges();
        }

        #region Disposing logic

        private static bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed || !disposing) return;

            DbContext.Dispose();
            _disposed = true;
        }

        #endregion Disposing logic
    }
}