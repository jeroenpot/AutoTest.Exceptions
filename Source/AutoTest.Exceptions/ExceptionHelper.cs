﻿using System;

using AutoTest.Exceptions.Exceptions;

namespace AutoTest.Exceptions
{
    internal static class ExceptionHelper
    {
        internal static void ThrowAutoTestException(Type exceptionType, string unformattedMessage)
        {
            ThrowAutoTestException(exceptionType, unformattedMessage, null);
        }

        internal static void ThrowAutoTestException(Type exceptionType, string unformattedMessage, Exception exception)
        {
            string exceptionMessage = string.Format(unformattedMessage, exceptionType);
            
            if (exception == null)
            {
                throw new AutoTestException(exceptionMessage);
            }

            throw new AutoTestException(exceptionMessage, exception);
        }
    }
}
