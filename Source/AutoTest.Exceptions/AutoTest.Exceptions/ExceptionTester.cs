using System;
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

        public static void TestAllExceptions(Assembly assembly)
        {
            new ExceptionTester().TestExceptions(assembly);
        }

        public void TestExceptions(Assembly assembly)
        {
            IList<Type> exceptions = _exceptionResolver.GetExceptions(assembly);

            TestExceptionTypes(exceptions);
        }

        internal void TestExceptionTypes(IEnumerable<Type> exceptions)
        {
            foreach (Type exceptionType in exceptions)
            {
                TestExceptionOfType(exceptionType);
            }
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
