using Vetsus.Domain.Utilities;

namespace Vetsus.Domain.Errors
{
    public static partial class Errors
    {
        public static class User
        {
            public static GenericError IncorrectEmailOrPassword(string? msg = null) => new("VTS002", msg ?? "Email o Contraseña incorrecto.");            
            public static GenericError UserDoesNotExist(string? msg = null) => new("VTS001", msg ?? "Usuario no existente.");
        }
    }
}
