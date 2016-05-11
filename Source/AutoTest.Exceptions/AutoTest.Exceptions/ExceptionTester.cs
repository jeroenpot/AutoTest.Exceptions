using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using AutoTest.Exceptions.Exceptions;

namespace AutoTest.Exceptions
{
    /// <summary>
    /// Class for testing your exceptions.
    /// </summary>
    public class ExceptionTester
    {
        private readonly IExceptionResolver _exceptionResolver;

        private readonly ISerializationHelper _serializationHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionTester"/> class.
        /// </summary>
        public ExceptionTester()
            : this(new ExceptionResolver(), new SerializationHelper())
        {
        }

        // ReSharper disable once MemberCanBePrivate.Global
        internal ExceptionTester(IExceptionResolver exceptionResolver, ISerializationHelper serializationHelper)
        {
            _exceptionResolver = exceptionResolver;
            _serializationHelper = serializationHelper;
        }

        /// <summary>
        /// Tests all exceptions.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="exceptionsToIgnore">The exceptions to ignore.</param>
        /// <returns>
        /// A dictionary containing all the tested exceptions.
        /// </returns>
        public static IList<ResultMessage> TestAllExceptions(Assembly assembly, params Type[] exceptionsToIgnore)
        {
            return new ExceptionTester().TestExceptions(assembly, exceptionsToIgnore);
        }

        /// <summary>
        /// Tests all exceptions.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="exceptionsToIgnore">The exceptions to ignore.</param>
        /// <returns>
        /// A dictionary containing all the tested exceptions.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">assembly</exception>
        // ReSharper disable once MemberCanBePrivate.Global
        internal IList<ResultMessage> TestExceptions(Assembly assembly, params Type[] exceptionsToIgnore)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            IList<Type> exceptions = _exceptionResolver.GetExceptions(assembly, exceptionsToIgnore);

            return TestExceptionTypes(exceptions);
        }

        // ReSharper disable once MemberCanBePrivate.Global
        internal IList<ResultMessage> TestExceptionTypes(IEnumerable<Type> exceptions)
        {
            List<ResultMessage> messages = new List<ResultMessage>();

            foreach (Type exceptionType in exceptions)
            {
                IList<ResultMessage> resultMessages = TestExceptionOfType(exceptionType);
                messages.AddRange(resultMessages);
            }

            if (messages.Any(m => m.Success == false))
            {
                throw new AutoTestException("Not all exceptions could be tested successfully." + messages.CreateReadableList());
            }

            return messages;
        }

        internal IList<ResultMessage> TestExceptionOfType(Type exceptionType)
        {
            IList<ResultMessage> resultMessages = new List<ResultMessage>();

            resultMessages.Add(DefaultConstructorTest(exceptionType));
            resultMessages.Add(ConstructorWithMessageTest(exceptionType));
            resultMessages.Add(ConstructorWithMessageAndInnerExceptionTest(exceptionType));

            if (resultMessages.All(m => m.Success))
            {
                resultMessages.Add(SerializationTest(exceptionType));
            }

            return resultMessages;
        }

        private ResultMessage SerializationTest(Type exceptionType)
        {
            Exception exception = Activator.CreateInstance(exceptionType) as Exception;
            return _serializationHelper.SerializeAndDeserializeException(exception);
        }

        private ResultMessage DefaultConstructorTest(Type exceptionType)
        {
            ResultMessage resultMessage = new ResultMessage(exceptionType);

#pragma warning disable 219
            // ReSharper disable once NotAccessedVariable
            Exception createdException = null;
#pragma warning restore 219
            try
            {
                // ReSharper disable once RedundantAssignment
                createdException = Activator.CreateInstance(exceptionType) as Exception;
            }
            catch (Exception exception)
            {
                resultMessage = ResultMessageBuilder.ResultMessageForException(exceptionType, Properties.Resources.FailedToCreateExceptionParameterless, exception);
            }

            return resultMessage;
        }

        private ResultMessage ConstructorWithMessageTest(Type exceptionType)
        {
            ResultMessage resultMessage = new ResultMessage(exceptionType);
#pragma warning disable 219
            // ReSharper disable once NotAccessedVariable
            Exception createdException = null;
#pragma warning restore 219
            try
            {
                // ReSharper disable once RedundantAssignment
                createdException = Activator.CreateInstance(exceptionType, "ExceptionMessage") as Exception;
            }
            catch (Exception exception)
            {
                resultMessage = ResultMessageBuilder.ResultMessageForException(exceptionType, Properties.Resources.FailedToCreateExceptionParameterMessage, exception);
            }

            return resultMessage;
        }

        private ResultMessage ConstructorWithMessageAndInnerExceptionTest(Type exceptionType)
        {
            ResultMessage resultMessage = new ResultMessage(exceptionType);
#pragma warning disable 219
            // ReSharper disable once NotAccessedVariable
            Exception createdException = null;
#pragma warning restore 219
            try
            {
                const string message = "ExceptionMessage";
                Exception innerException = new Exception("Inner exception");
                // ReSharper disable once RedundantAssignment
                createdException = Activator.CreateInstance(exceptionType, message, innerException) as Exception;
            }
            catch (Exception exception)
            {
                resultMessage = ResultMessageBuilder.ResultMessageForException(exceptionType, Properties.Resources.FailedToCreateExceptionParameterMessageAndInnerException, exception);
            }

            return resultMessage;
        }
    }
}
