using System;
using System.Runtime.Serialization;

namespace ExampleProject
{
    [Serializable]
    // ReSharper disable once UnusedMember.Global
    public class MyException : Exception
    {
        public MyException()
        {
        }

        public MyException(string message)
            : base(message)
        {
        }

        public MyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public MyException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
