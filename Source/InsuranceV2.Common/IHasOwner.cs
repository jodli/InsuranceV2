namespace InsuranceV2.Common
{
    public interface IHasOwner<T>
    {
        T Owner { get; set; }
    }
}