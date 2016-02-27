using System;
using Prism.Mvvm;

namespace InsuranceV2.Common.MVVM
{
    public abstract class DisposableViewModel : BindableBase, IDisposable
    {
        public bool IsDisposed { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DisposableViewModel()
        {
            Dispose(false);
        }

        protected virtual void DisposeManaged()
        {
        }

        protected virtual void DisposeUnmanaged()
        {
        }

        private void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    DisposeManaged();
                }

                DisposeUnmanaged();

                IsDisposed = true;
            }
        }
    }
}