using System;
using System.Collections.Generic;
using System.Reflection;

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
    }
}
