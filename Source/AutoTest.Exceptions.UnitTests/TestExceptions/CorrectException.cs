using System;
using System.Runtime.Serialization;

namespace AutoTest.Exceptions.UnitTests.TestExceptions
{
    [Serializable]
    public class CorrectException : Exception
    {
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        public CorrectException()
        {
        }

        public CorrectException(string message)
            : base(message)
        {
        }

        public CorrectException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected CorrectException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}