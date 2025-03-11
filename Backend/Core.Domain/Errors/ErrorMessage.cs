namespace Core.Domain.Errors
{
    public struct ErrorMessage
    {
        public const string SUC000 = "OK.";
        public const string VAL001 = "Se produjeron uno o más errores de validación.";
        public const string ERR001 = "¡Oh no! Parece que hubo un problema al completar tu solicitud. Por favor, vuelve a intentarlo más tarde.";
        public const string AUT001 = "No existe una sesión iniciada para el usuario.";
        public const string AUT002 = "El token de seguridad no es válido.";
        public const string AUT003 = "El usuario no tiene acceso al recurso solicitado.";
    }
}
