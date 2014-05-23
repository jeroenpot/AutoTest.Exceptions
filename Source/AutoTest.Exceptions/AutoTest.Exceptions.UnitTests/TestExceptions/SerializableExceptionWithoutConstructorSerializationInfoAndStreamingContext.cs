using System;

namespace AutoTest.Exceptions.UnitTests.TestExceptions
{
    [Serializable]
    public class SerializableExceptionWithoutConstructorSerializationInfoAndStreamingContext : Exception
    {
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
