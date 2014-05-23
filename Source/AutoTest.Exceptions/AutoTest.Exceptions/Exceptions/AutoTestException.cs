using System;
using System.Runtime.Serialization;

namespace AutoTest.Exceptions.Exceptions
{
    /// <summary>
    /// Exception that is thrown if an exception cannot be tested successfully.
    /// </summary>
    [Serializable]
    public class AutoTestException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoTestException"/> class.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        public AutoTestException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoTestException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public AutoTestException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoTestException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public AutoTestException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoTestException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected AutoTestException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
