using System;

namespace AutoTest.Exceptions.UnitTests.TestExceptions
{
    public class ExceptionWithoutParameterlessConstructor : Exception
    {
        public ExceptionWithoutParameterlessConstructor(string message)
            : base(message)
        {
        }
    }
}