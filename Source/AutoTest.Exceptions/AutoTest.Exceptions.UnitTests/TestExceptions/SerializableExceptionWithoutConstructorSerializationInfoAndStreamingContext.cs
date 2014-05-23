using System;

namespace AutoTest.Exceptions.UnitTests.TestExceptions
{
    [Serializable]
    public class SerializableExceptionWithoutConstructorSerializationInfoAndStreamingContext : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public SerializableExceptionWithoutConstructorSerializationInfoAndStreamingContext()
        {
        }

        public SerializableExceptionWithoutConstructorSerializationInfoAndStreamingContext(string message)
            : base(message)
        {
        }

        public SerializableExceptionWithoutConstructorSerializationInfoAndStreamingContext(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
