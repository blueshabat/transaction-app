using FluentValidation;

namespace Transaction.Domain.Models.Requests
{
    public class TransactionRequestValidator : AbstractValidator<TransactionRequest>
    {
        public TransactionRequestValidator()
        {
            RuleFor(transaction => transaction.Amount).Equal(0).WithMessage("El monto de la transacción no puede ser cero");
            RuleFor(transaction => transaction.Description).Length(0, 255).WithMessage("La descripción no puede superar los 255 caracteres");
            RuleFor(transaction => transaction.Date).NotEmpty().NotNull().WithMessage("La fecha de la transacción no puede estar vacía");
        }
    }
}
