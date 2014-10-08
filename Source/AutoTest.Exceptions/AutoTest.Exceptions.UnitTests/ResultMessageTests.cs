using System;

using NUnit.Framework;

namespace AutoTest.Exceptions.UnitTests
{
    [TestFixture]
    public class ResultMessageTests
    {
        [Test]
        public void ShouldThrowExceptionWhenArgumentIsNull()
        {
            Exception exception = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(() => new ResultMessage(exception));
        }
    }
}
