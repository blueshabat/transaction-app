using Core.Domain.Models;
using Transaction.Domain.Models.Requests;

namespace Transaction.Application.Contracts
{
    public interface ITransaction
    {
        Task<BaseResponse<List<Domain.Entities.Transaction>>> List(CancellationToken cancellationToken = default);

        Task<BaseResponse<int>> Create(TransactionRequest transactionRequest, CancellationToken cancellationToken = default);

        Task<BaseResponse<int>> Update(TransactionRequest transactionRequest, CancellationToken cancellationToken = default);

        Task<BaseResponse<int>> Delete(TransactionIdRequest transactionIdRequest, CancellationToken cancellationToken = default);

        Task<BaseResponse<Domain.Entities.Transaction>> GetById(TransactionIdRequest transactionIdRequest, CancellationToken cancellationToken = default);
    }
}
