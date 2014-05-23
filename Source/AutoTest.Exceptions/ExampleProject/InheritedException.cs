using System;
using System.Runtime.Serialization;

namespace ExampleProject
{
    [Serializable]
    public class InheritedException : TestException
    {
        public InheritedException()
        {
        }

        public InheritedException(string message)
            : base(message)
        {
        }

        public InheritedException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected InheritedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
