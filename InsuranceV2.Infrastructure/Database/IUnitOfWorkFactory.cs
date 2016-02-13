namespace InsuranceV2.Infrastructure.Database
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();

        IUnitOfWork Create(bool forceNew);
    }
}