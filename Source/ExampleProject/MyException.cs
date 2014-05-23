using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExampleProject
{
    [Serializable]
    public class MyException : Exception
    {
        public MyException()
        {
            
        }

        public MyException(string message)
            : base(message)
        {
        }

        public MyException(string message, Exception innerException):base(message, innerException)
        {
            
        }

        public MyException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
            
        }
    }
}
