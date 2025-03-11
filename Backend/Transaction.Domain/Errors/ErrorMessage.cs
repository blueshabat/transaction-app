namespace Transaction.Domain.Errors
{
    public struct ErrorMessage
    {
        public const string ERR001 = "La transacción no existe";
        public const string ERR002 = "El monto del débito no puede superar el saldo actual";
    }
}
