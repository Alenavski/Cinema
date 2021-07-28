using System;

namespace Cinema.Services.Exceptions
{
    public class AuthenticationDataInvalidException: Exception
    {
        public AuthenticationDataInvalidException(string message) : base(message)
        {
            
        }
    }
}