using System;

namespace InsuranceV2.Infrastructure.Database
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit(bool resetAfterCommit);

        void Undo();
    }
}