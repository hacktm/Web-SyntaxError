using System;

namespace CollegeBuffer.DAL.Context
{
    public class BaseUnitOfWork
    {
        public BaseUnitOfWork()
        {
            _disposed = false;
        }

        public DatabaseContext DbContext { get; private set; }

        #region Disposing logic

        private static bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed || !disposing) return;

            DbContext.Dispose();
            _disposed = true;
        }

        #endregion Disposing logic
    }
}