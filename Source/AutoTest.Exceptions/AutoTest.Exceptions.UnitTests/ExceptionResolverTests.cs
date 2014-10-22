using System;
using System.Collections.Generic;
using System.Reflection;

using AutoTest.Exceptions.UnitTests.TestExceptions;

using NUnit.Framework;

namespace AutoTest.Exceptions.UnitTests
{
    [TestFixture]
    public class ExceptionResolverTests
    {
        private readonly IExceptionResolver _exceptionResolver = new ExceptionResolver();

        [Test]
        public void ShouldFindException()
        {
            IList<Type> exceptions = _exceptionResolver.GetExceptions(Assembly.GetAssembly(typeof(ExceptionTesterTests)));

            Assert.That(exceptions, Has.Count.EqualTo(6));
        }

        [Test]
        public void ShouldIgnoreExceptionsThatAreGiven()
        {
            IList<Type> exceptions = _exceptionResolver.GetExceptions(Assembly.GetAssembly(typeof(ExceptionTesterTests)), typeof(ExceptionWithoutConstructorArgumentsMessageAndInnerException));

            Assert.That(exceptions, Has.Count.EqualTo(5));
        }

        [Test]
        public void ShouldIgnoreExceptionsList()
        {
            List<Type> types = new List<Type>();
            types.Add(typeof(ExceptionWithoutConstructorArgumentsMessageAndInnerException));
            IList<Type> exceptions = _exceptionResolver.GetExceptions(Assembly.GetAssembly(typeof(ExceptionTesterTests)), types);

            Assert.That(exceptions, Has.Count.EqualTo(5));
        }
    }
}
