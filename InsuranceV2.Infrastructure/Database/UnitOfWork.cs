using InsuranceV2.Infrastructure.Database.Context;

namespace InsuranceV2.Infrastructure.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(bool forceNewContext)
        {
            if (forceNewContext)
            {
                DataContextFactory.Clear();
            }
        }

        public void Commit(bool resetAfterCommit)
        {
            DataContextFactory.GetDataContext().SaveChanges();

            if (resetAfterCommit)
            {
                DataContextFactory.Clear();
            }
        }

        public void Undo()
        {
            DataContextFactory.Clear();
        }

        public void Dispose()
        {
            DataContextFactory.GetDataContext().SaveChanges();
        }
    }
}