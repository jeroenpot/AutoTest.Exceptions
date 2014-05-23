using System;

namespace AutoTest.Exceptions.UnitTests.TestExceptions
{
    public class NonSerializableException : Exception
    {
        public NonSerializableException()
        {
        }

        public NonSerializableException(string message)
            : base(message)
        {
        }

        public NonSerializableException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
