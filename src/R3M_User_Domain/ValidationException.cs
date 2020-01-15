using System;

namespace R3M_User_Domain.Apoio
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
        }
    }
}