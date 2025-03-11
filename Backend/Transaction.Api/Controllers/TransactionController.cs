using Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Transaction.Application.Contracts;
using Transaction.Domain.Models.Requests;

namespace Transaction.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TransactionController(ITransaction transaction) : ControllerBase
    {
        [HttpGet]
        public async Task<BaseResponse<List<Domain.Entities.Transaction>>> List(CancellationToken cancellationToken= default) =>
            await transaction.List(cancellationToken);

        [HttpPost]
        public async Task<BaseResponse<Domain.Entities.Transaction>> GetById(TransactionIdRequest transactionIdRequest, CancellationToken cancellationToken = default) =>
            await transaction.GetById(transactionIdRequest, cancellationToken);

        [HttpPost]
        public async Task<BaseResponse<int>> Create(TransactionRequest transactionRequest, CancellationToken cancellationToken = default) =>
            await transaction.Create(transactionRequest, cancellationToken);

        [HttpPut]
        public async Task<BaseResponse<int>> Update(TransactionRequest transactionRequest, CancellationToken cancellationToken = default) =>
            await transaction.Update(transactionRequest, cancellationToken);

        [HttpDelete]
        public async Task<BaseResponse<int>> Delete(TransactionIdRequest transactionIdRequest, CancellationToken cancellationToken = default) =>
            await transaction.Delete(transactionIdRequest, cancellationToken);

    }
}
