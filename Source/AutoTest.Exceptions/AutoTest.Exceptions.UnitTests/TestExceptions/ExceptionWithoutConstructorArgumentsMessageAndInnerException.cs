using System;

namespace AutoTest.Exceptions.UnitTests.TestExceptions
{
    [Serializable]
    public class ExceptionWithoutConstructorArgumentsMessageAndInnerException : Exception
    {
        public ExceptionWithoutConstructorArgumentsMessageAndInnerException()
        {
        }

        public ExceptionWithoutConstructorArgumentsMessageAndInnerException(string message)
            : base(message)
        {
        }
    }
}
