using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

using AutoTest.Exceptions.Exceptions;
using AutoTest.Exceptions.UnitTests.TestExceptions;

using NUnit.Framework;

namespace AutoTest.Exceptions.UnitTests
{
    [TestFixture]
    public class ExceptionTesterTests
    {
        private readonly ExceptionTester _exceptionTester = new ExceptionTester();

        [Test]
        public void ShouldNotThrowExceptionWhenExceptionIsCorrect()
        {
            _exceptionTester.TestExceptionOfType(typeof(CorrectException));
        }

        [Test]
        public void ShouldThrowExceptionWhenNoParameterlessConstructorIsFound()
        {
            Assert.That(
                () => _exceptionTester.TestExceptionOfType(typeof(ExceptionWithoutParameterlessConstructor)),
                Throws.Exception.TypeOf<AutoTestException>().With.Message.EqualTo("Failed to create exception of type AutoTest.Exceptions.UnitTests.TestExceptions.ExceptionWithoutParameterlessConstructor with parameterless constructor"));
        }

        [Test]
        public void ShouldThrowExceptionWhenNoConstructorWithArgumentMessageIsFound()
        {
            Assert.That(
                () => _exceptionTester.TestExceptionOfType(typeof(ExceptionWithoutConstructorWithArgumentMessage)),
                Throws.Exception.TypeOf<AutoTestException>()
                    .With.InnerException.TypeOf<MissingMethodException>()
                    .With.Message.EqualTo("Failed to create exception of type AutoTest.Exceptions.UnitTests.TestExceptions.ExceptionWithoutConstructorWithArgumentMessage with constructor parameter string message"));
        }

        [Test]
        public void ShouldThrowExceptionWhenNoConstructorWithArgumentMessageAndArgumentInnerExceptionIsFound()
        {
            Assert.That(
                () => _exceptionTester.TestExceptionOfType(typeof(ExceptionWithoutConstructorArgumentsMessageAndInnerException)),
                Throws.Exception.TypeOf<AutoTestException>()
                .With.InnerException.TypeOf<MissingMethodException>()
                .With.Message.EqualTo("Failed to create exception of type AutoTest.Exceptions.UnitTests.TestExceptions.ExceptionWithoutConstructorArgumentsMessageAndInnerException with constructor parameter string message and parameter innerException"));
        }

        [Test]
        public void ShouldThrowExceptionWhenExceptionIsNotSerializable()
        {
            Assert.That(
                () => _exceptionTester.TestExceptionOfType(typeof(NonSerializableException)),
                Throws.Exception.TypeOf<AutoTestException>()
                .With.InnerException.TypeOf<SerializationException>()
                .With.Message.EqualTo("Failed to serialize exception of type AutoTest.Exceptions.UnitTests.TestExceptions.NonSerializableException"));
        }

        [Test]
        public void ShouldThrowExceptionWhenExceptionSerializableAndDoesNotHaveConstructorWithSerializationInfoAndStreamingContext()
        {
            Assert.That(
                () => _exceptionTester.TestExceptionOfType(typeof(SerializableExceptionWithoutConstructorSerializationInfoAndStreamingContext)),
                Throws.Exception.TypeOf<AutoTestException>()
                .With.InnerException.TypeOf<SerializationException>()
                .With.Message.EqualTo("Failed to deserialize exception of type AutoTest.Exceptions.UnitTests.TestExceptions.SerializableExceptionWithoutConstructorSerializationInfoAndStreamingContext"));
        }

        [Test]
        public void ShouldTestAllExceptionsInAutoTestExceptions()
        {
            IDictionary<Type, string> result = ExceptionTester.TestAllExceptions(Assembly.GetAssembly(typeof(ExceptionTester)));

            Assert.That(result, Has.Count.EqualTo(1));
        }
    }
}
