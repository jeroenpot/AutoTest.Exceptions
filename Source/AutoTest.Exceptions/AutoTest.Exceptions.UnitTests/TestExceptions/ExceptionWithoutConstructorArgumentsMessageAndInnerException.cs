using System;

namespace AutoTest.Exceptions.UnitTests.TestExceptions
{
    public class ExceptionWithoutConstructorArgumentsMessageAndInnerException : Exception
    {
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp


        public ExceptionWithoutConstructorArgumentsMessageAndInnerException()
        {
        }

        public ExceptionWithoutConstructorArgumentsMessageAndInnerException(string message)
            : base(message)
        {
        }
    }
}
