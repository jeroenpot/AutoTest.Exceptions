using System;
using System.Runtime.Serialization;

namespace ExampleProject
{
    [Serializable]
    public class TestException : Exception
    {
        public TestException()
        {
        }

        public TestException(string message)
            : base(message)
        {
        }

        public TestException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected TestException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
