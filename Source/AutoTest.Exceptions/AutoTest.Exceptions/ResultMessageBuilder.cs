using System;
using System.Globalization;

namespace AutoTest.Exceptions
{
    internal static class ResultMessageBuilder
    {
        internal static ResultMessage ResultMessageForException(Type exceptionType, string unformattedMessage, Exception exception)
        {
            ResultMessage resultMessage = new ResultMessage(exception);
            resultMessage.Success = false;
            resultMessage.Message = string.Format(CultureInfo.InvariantCulture, unformattedMessage, exceptionType);

            return resultMessage;
        }
    }
}
