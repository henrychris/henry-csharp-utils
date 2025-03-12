namespace HenryUtils.UnitOfWork.Base
{
    public interface IUnitOfWork : IDisposable
    {
        // IPlaceholderRepository Placeholders { get; }
        int Complete();
        Task<int> CompleteAsync();
    }
}
