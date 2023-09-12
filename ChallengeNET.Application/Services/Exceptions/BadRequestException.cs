using System;

namespace EjercicioPOO.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string errorMessage) : base(errorMessage)
        {

        }
    }
}
