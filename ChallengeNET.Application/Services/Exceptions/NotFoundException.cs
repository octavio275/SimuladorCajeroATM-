﻿using System;

namespace EjercicioPOO.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string errorMessage) : base(errorMessage)
        {

        }
    }
}
