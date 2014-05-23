using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace AutoTest.Exceptions
{
    public class ExceptionTester
    {
        private readonly IExceptionResolver _exceptionResolver;

        private readonly ISerializationHelper _serializationHelper;

        public ExceptionTester()
            : this(new ExceptionResolver(), new SerializationHelper())
        {
        }

        internal ExceptionTester(IExceptionResolver exceptionResolver, ISerializationHelper serializationHelper)
        {
            _exceptionResolver = exceptionResolver;
            _serializationHelper = serializationHelper;
        }

        public static IDictionary<Type, string> TestAllExceptions(Assembly assembly)
        {
            return new ExceptionTester().TestExceptions(assembly);
        }

        public IDictionary<Type, string> TestExceptions(Assembly assembly)
        {
            IList<Type> exceptions = _exceptionResolver.GetExceptions(assembly);

            return TestExceptionTypes(exceptions);
        }

        internal IDictionary<Type, string> TestExceptionTypes(IEnumerable<Type> exceptions)
        {
            IDictionary<Type, string> result = new ConcurrentDictionary<Type, string>();

            foreach (Type exceptionType in exceptions)
            {
                TestExceptionOfType(exceptionType);
                result.Add(exceptionType, string.Format("Tested exception of type [{0}] successfully", exceptionType));
            }

            return result;
        }

        internal void TestExceptionOfType(Type exceptionType)
        {
            Exception defaultConstructor = DefaultConstructorTest(exceptionType);
            Exception constructorWithMessage = ConstructorWithMessageTest(exceptionType);
            Exception constructorWithMessageAndInnerException = ConstructorWithMessageAndInnerExceptionTest(exceptionType);

            SerializationTest(defaultConstructor, exceptionType);
            SerializationTest(constructorWithMessage, exceptionType);
            SerializationTest(constructorWithMessageAndInnerException, exceptionType);
        }

        private void SerializationTest(Exception exception, Type excecptionType)
        {
            Exception deserializedException = _serializationHelper.SerializeAndDeserializeException(exception);
            if (deserializedException == null)
            {
                ExceptionHelper.ThrowAutoTestException(excecptionType, Properties.Resources.ExceptionIsNullAfterSerializationAndDeserialization);
            }
        }

        private Exception DefaultConstructorTest(Type exceptionType)
        {
            Exception createdException = null;
            try
            {
                createdException = Activator.CreateInstance(exceptionType) as Exception;
            }
            catch (Exception exception)
            {
                ExceptionHelper.ThrowAutoTestException(exceptionType, Properties.Resources.FailedToCreateExceptionParameterless, exception);
            }

            return createdException;
        }

        private Exception ConstructorWithMessageTest(Type exceptionType)
        {
            Exception createdException = null;
            try
            {
                createdException = Activator.CreateInstance(exceptionType, "ExceptionMessage") as Exception;
            }
            catch (Exception exception)
            {
                ExceptionHelper.ThrowAutoTestException(exceptionType, Properties.Resources.FailedToCreateExceptionParameterMessage, exception);
            }

            return createdException;
        }

        private Exception ConstructorWithMessageAndInnerExceptionTest(Type exceptionType)
        {
            Exception createdException = null;
            try
            {
                const string Message = "ExceptionMessage";
                Exception innerException = new Exception("InnerException");
                createdException = Activator.CreateInstance(exceptionType, Message, innerException) as Exception;
            }
            catch (Exception exception)
            {
                ExceptionHelper.ThrowAutoTestException(exceptionType, Properties.Resources.FailedToCreateExceptionParameterMessageAndInnerException, exception);
            }

            return createdException;
        }
    }
}
