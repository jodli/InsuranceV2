namespace InsuranceV2.Infrastructure.Database
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork Create()
        {
            return Create(false);
        }

        public IUnitOfWork Create(bool forceNew)
        {
            return new UnitOfWork(forceNew);
        }
    }
}