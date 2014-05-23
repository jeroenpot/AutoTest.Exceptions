using System;
using System.Runtime.Serialization;

namespace AutoTest.Exceptions.Exceptions
{
    [Serializable]
    public class AutoTestException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public AutoTestException()
        {
        }

        public AutoTestException(string message)
            : base(message)
        {
        }

        public AutoTestException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected AutoTestException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
