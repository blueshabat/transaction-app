using FluentValidation;

namespace Transaction.Domain.Models.Requests
{
    public class TransactionIdRequestValidator : AbstractValidator<TransactionIdRequest>
    {
        public TransactionIdRequestValidator()
        {
            RuleFor(transaction => transaction.TransactionId).NotNull().NotEmpty().WithMessage("El identificador de la transacción es requerido");
        }
    }
}
