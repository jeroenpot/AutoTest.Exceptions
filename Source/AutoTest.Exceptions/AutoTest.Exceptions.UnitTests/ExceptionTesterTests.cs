using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

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
            IList<ResultMessage> resultMessages = _exceptionTester.TestExceptionOfType(typeof(ExceptionWithoutParameterlessConstructor));

            Assert.That(resultMessages, Has.Count.EqualTo(3));

            IList<ResultMessage> failures = resultMessages.Where(m => m.Success == false).ToList();

            Assert.That(failures, Has.Count.EqualTo(2));
            ResultMessage firstFailure = failures[0];
            ResultMessage secondFailure = failures[1];

            Assert.That(firstFailure.Exception, Is.TypeOf<MissingMethodException>());
            Assert.That(firstFailure.Message, Is.EqualTo("Failed to create exception of type AutoTest.Exceptions.UnitTests.TestExceptions.ExceptionWithoutParameterlessConstructor with parameterless constructor"));
            Assert.That(secondFailure.Exception, Is.TypeOf<MissingMethodException>());
            Assert.That(secondFailure.Message, Is.EqualTo("Failed to create exception of type AutoTest.Exceptions.UnitTests.TestExceptions.ExceptionWithoutParameterlessConstructor with constructor parameter string message and parameter innerException"));
        }

        [Test]
        public void ShouldThrowExceptionWhenNoConstructorWithArgumentMessageIsFound()
        {
            IList<ResultMessage> resultMessages = _exceptionTester.TestExceptionOfType(typeof(ExceptionWithoutConstructorWithArgumentMessage));

            Assert.That(resultMessages, Has.Count.EqualTo(3));

            IList<ResultMessage> failures = resultMessages.Where(m => m.Success == false).ToList();

            Assert.That(failures, Has.Count.EqualTo(2));
            ResultMessage firstFailure = failures[0];
            ResultMessage secondFailure = failures[1];

            Assert.That(firstFailure.Exception, Is.TypeOf<MissingMethodException>());
            Assert.That(firstFailure.Message, Is.EqualTo("Failed to create exception of type AutoTest.Exceptions.UnitTests.TestExceptions.ExceptionWithoutConstructorWithArgumentMessage with constructor parameter string message"));
            Assert.That(secondFailure.Exception, Is.TypeOf<MissingMethodException>());
            Assert.That(secondFailure.Message, Is.EqualTo("Failed to create exception of type AutoTest.Exceptions.UnitTests.TestExceptions.ExceptionWithoutConstructorWithArgumentMessage with constructor parameter string message and parameter innerException"));
        }

        [Test]
        public void ShouldThrowExceptionWhenNoConstructorWithArgumentMessageAndArgumentInnerExceptionIsFound()
        {
            IList<ResultMessage> resultMessages = _exceptionTester.TestExceptionOfType(typeof(NonSerializableException));

            AssertResultMessageContainsSingleFailure(
                resultMessages,
                typeof(SerializationException),
                "Failed to serialize exception of type AutoTest.Exceptions.UnitTests.TestExceptions.NonSerializableException");
        }

        [Test]
        public void ShouldThrowExceptionWhenExceptionIsNotSerializable()
        {
            IList<ResultMessage> resultMessages = _exceptionTester.TestExceptionOfType(typeof(NonSerializableException));

            AssertResultMessageContainsSingleFailure(
                resultMessages,
                typeof(SerializationException),
                "Failed to serialize exception of type AutoTest.Exceptions.UnitTests.TestExceptions.NonSerializableException");
        }

        [Test]
        public void ShouldThrowExceptionWhenExceptionSerializableAndDoesNotHaveConstructorWithSerializationInfoAndStreamingContext()
        {
            IList<ResultMessage> resultMessages = _exceptionTester.TestExceptionOfType(typeof(SerializableExceptionWithoutConstructorSerializationInfoAndStreamingContext));

            AssertResultMessageContainsSingleFailure(
                resultMessages,
                typeof(SerializationException),
                "Failed to deserialize exception of type AutoTest.Exceptions.UnitTests.TestExceptions.SerializableExceptionWithoutConstructorSerializationInfoAndStreamingContext");
        }

        [Test]
        public void ShouldTestAllExceptionsInAutoTestExceptions()
        {
            IList<ResultMessage> result = ExceptionTester.TestAllExceptions(Assembly.GetAssembly(typeof(ExceptionTester)));

            Assert.That(result, Has.Count.EqualTo(4));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWhenAssemblyIsNull()
        {
            Assert.That(() => _exceptionTester.TestExceptions(null), Throws.Exception.TypeOf<ArgumentNullException>());
        }

        private void AssertResultMessageContainsSingleFailure(IEnumerable<ResultMessage> resultMessages, Type excpectedExceptionType, string exceptionMessage)
        {
            Assert.That(resultMessages, Has.Count.EqualTo(4));

            IList<ResultMessage> failures = resultMessages.Where(m => m.Success == false).ToList();

            Assert.That(failures, Has.Count.EqualTo(1));
            ResultMessage resultMessage = failures.First();

            Assert.That(resultMessage.Exception, Is.TypeOf(excpectedExceptionType));
            Assert.That(resultMessage.Message, Is.EqualTo(exceptionMessage));
        }
    }
}
