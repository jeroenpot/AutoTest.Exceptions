using System;

namespace AutoTest.Exceptions
{
    /// <summary>
    /// Object for storing result of testing the exception.
    /// </summary>
    public class ResultMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResultMessage"/> class.
        /// </summary>
        /// <param name="exceptionType">Type of the exception.</param>
        public ResultMessage(Type exceptionType)
            : this()
        {
            ExceptionType = exceptionType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultMessage"/> class.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public ResultMessage(Exception exception)
            : this()
        {
            if (exception == null)
            {
                throw new ArgumentNullException("exception");
            }

            Exception = exception;
            ExceptionType = exception.GetType();
        }

        private ResultMessage()
        {
            Success = true;
        }

        /// <summary>
        /// Gets or sets the type of the exception.
        /// </summary>
        /// <value>
        /// The type of the exception.
        /// </value>
        public Type ExceptionType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ResultMessage"/> is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        public Exception Exception { get; set; }
    }
}
