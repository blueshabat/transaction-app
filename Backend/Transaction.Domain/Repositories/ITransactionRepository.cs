namespace Transaction.Domain.Repositories
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Entities.Transaction>> List(CancellationToken cancellationToken = default);

        Task<int> Create(Entities.Transaction transaction, CancellationToken cancellationToken = default);

        Task<int> Update(Entities.Transaction transaction, CancellationToken cancellationToken = default);

        Task<int> Delete(int id, CancellationToken cancellationToken = default);

        Task<Entities.Transaction?> GetById(int id, CancellationToken cancellationToken = default);

        Task<Entities.Transaction?> GetLast(CancellationToken cancellationToken = default);
    }
}
