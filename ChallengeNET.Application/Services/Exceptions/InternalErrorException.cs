using System;

namespace EjercicioPOO.Application.Exceptions
{
    public class InternalErrorException : Exception
    {
        public InternalErrorException(string errorMesagge) : base(errorMesagge)
        {

        }
    }
}
