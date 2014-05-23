using System.Reflection;

using AutoTest.Exceptions;

using NUnit.Framework;

namespace ExampleProject.UnitTests
{
    [TestFixture]
    public class ExampleTest
    {
        [Test]
        public void TestAllMyCustomExceptions()
        {
            ExceptionTester.TestAllExceptions(Assembly.GetAssembly(typeof(TestException)));
        }
    }
}
