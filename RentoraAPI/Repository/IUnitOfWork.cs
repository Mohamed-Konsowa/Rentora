namespace RentoraAPI.Repository
{
    public interface IUnitOfWork
    {
        ProductRepository products { get; }
        void Save();
    }
}
