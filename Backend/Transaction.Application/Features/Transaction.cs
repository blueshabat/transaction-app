using Core.Domain.Models;
using Transaction.Application.Contracts;
using Transaction.Domain.Enums;
using Transaction.Domain.Errors;
using Transaction.Domain.Models.Requests;
using Transaction.Domain.Repositories;

namespace Transaction.Application.Features
{
    public class Transaction(ITransactionRepository transactionRepository) : ITransaction
    {
        private readonly Dictionary<TransactionType, string> transactionTypes = new()
        {
            { TransactionType.Debit, "Gasto" },
            { TransactionType.Credit, "Ingreso" },
        };

        public async Task<BaseResponse<int>> Create(TransactionRequest transactionRequest, CancellationToken cancellationToken = default)
        {
            var lastTransaction = await transactionRepository.GetLast(cancellationToken);
            if (lastTransaction != null && !ValidateLastBalance(transactionRequest.Type, transactionRequest.Amount, lastTransaction.Balance))
            {
                return BaseResponse<int>.Error(ErrorCode.ERR002, ErrorMessage.ERR002);
            }
            var response = await transactionRepository.Create(new Domain.Entities.Transaction
            {
                Amount = transactionRequest.Amount,
                Category = transactionRequest.Category,
                Date = transactionRequest.Date,
                Description = transactionRequest.Description,
                Type = transactionTypes[transactionRequest.Type],
                Balance = CalculateBalance(lastTransaction, transactionRequest.Type, transactionRequest.Amount)
            }, cancellationToken);
            return BaseResponse<int>.Success(response);
        }

        public async Task<BaseResponse<int>> Delete(TransactionIdRequest transactionIdRequest, CancellationToken cancellationToken = default)
        {
            if (!await ValidateTransactionExistence(transactionIdRequest.TransactionId, cancellationToken))
            {
                return BaseResponse<int>.Error(ErrorCode.ERR001, ErrorMessage.ERR001);
            }
            var response = await transactionRepository.Delete(transactionIdRequest.TransactionId, cancellationToken);
            return BaseResponse<int>.Success(response);
        }

        public async Task<BaseResponse<List<Domain.Entities.Transaction>>> List(CancellationToken cancellationToken = default)
        {
            var transactions = await transactionRepository.List(cancellationToken);
            return BaseResponse<List<Domain.Entities.Transaction>>.Success(transactions.ToList());
        }

        public async Task<BaseResponse<int>> Update(TransactionRequest transactionRequest, CancellationToken cancellationToken = default)
        {
            if (!await ValidateTransactionExistence(transactionRequest.Id, cancellationToken))
            {
                return BaseResponse<int>.Error(ErrorCode.ERR001, ErrorMessage.ERR001);
            }
            var lastTransaction = await transactionRepository.GetLast(cancellationToken);
            if (lastTransaction != null && !ValidateLastBalance(transactionRequest.Type, transactionRequest.Amount, lastTransaction.Balance))
            {
                return BaseResponse<int>.Error(ErrorCode.ERR002, ErrorMessage.ERR002);
            }
            var response = await transactionRepository.Update(new Domain.Entities.Transaction
            {
                Id = transactionRequest.Id,
                Amount = transactionRequest.Amount,
                Category = transactionRequest.Category,
                Date = transactionRequest.Date,
                Description = transactionRequest.Description,
                Type = transactionTypes[transactionRequest.Type],
                Balance = CalculateBalance(lastTransaction, transactionRequest.Type, transactionRequest.Amount)
            }, cancellationToken);
            return BaseResponse<int>.Success(response);
        }

        private async Task<bool> ValidateTransactionExistence(int id, CancellationToken cancellationToken = default) => 
            await transactionRepository.GetById(id, cancellationToken) != null;

        private static bool ValidateLastBalance(TransactionType transactionType, decimal transactionAmount, decimal lastBalance) =>
            transactionType == TransactionType.Credit || (transactionType == TransactionType.Debit && transactionAmount < lastBalance);

        private static decimal CalculateBalance(Domain.Entities.Transaction? lastTransaction, TransactionType transactionType, decimal transactionAmount)
        {
            if (lastTransaction == null)
            {
                return transactionAmount;
            }
            else
            {
                return lastTransaction.Balance + (transactionAmount * (transactionType == TransactionType.Debit ? -1 : 1));
            }
        }

        public async Task<BaseResponse<Domain.Entities.Transaction>> GetById(TransactionIdRequest transactionIdRequest, CancellationToken cancellationToken = default)
        {
            var response = await transactionRepository.GetById(transactionIdRequest.TransactionId, cancellationToken);
            return BaseResponse<Domain.Entities.Transaction>.Success(response);
        }
    }
}
