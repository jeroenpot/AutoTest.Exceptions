using System;
using System.Collections.Generic;
using System.Reflection;

using AutoTest.Exceptions;
using AutoTest.Exceptions.Exceptions;
using AutoTest.Exceptions.UnitTests.TestExceptions;

using NUnit.Framework;

namespace ExampleProject.UnitTests
{
    [TestFixture]
    public class ExampleTest
    {
        [Test]
        public void TestAllMyCustomExceptionsSuccessfull()
        {
            IList<ResultMessage> result = ExceptionTester.TestAllExceptions(Assembly.GetAssembly(typeof(TestException)));

            Assert.That(result.Count, Is.EqualTo(12));
        }

        [Test]
        public void ShouldThrowExceptionWhenNotAllExceptionsAreTestedSuccesfully()
        {
            Assert.Throws<AutoTestException>(() => ExceptionTester.TestAllExceptions(Assembly.GetAssembly(typeof(CorrectException))));
        }
    }
}
