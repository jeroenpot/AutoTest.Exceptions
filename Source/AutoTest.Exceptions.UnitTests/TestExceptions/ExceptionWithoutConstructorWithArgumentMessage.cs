using System;

namespace AutoTest.Exceptions.UnitTests.TestExceptions
{
    public class ExceptionWithoutConstructorWithArgumentMessage : Exception
    {
        public ExceptionWithoutConstructorWithArgumentMessage()
            : base()
        {
        }
    }
}