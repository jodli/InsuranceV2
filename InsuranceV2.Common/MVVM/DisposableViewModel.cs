using System;
using Prism;
using Prism.Mvvm;

namespace InsuranceV2.Common.MVVM
{
    public abstract class DisposableViewModel : BindableBase, IActiveAware, IDisposable
    {
        #region IActiveAware

        private bool _isActive;

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                _isActive = value;
                if (value)
                {
                    OnActivate();
                }
                else
                {
                    OnDeactivate();
                }
                IsActiveChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        protected abstract void OnDeactivate();

        protected abstract void OnActivate();

        public event EventHandler IsActiveChanged;

        #endregion

        #region IDisposable

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

        #endregion
    }
}