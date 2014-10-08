using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoTest.Exceptions
{
    /// <summary>
    /// Formats a list of resultmessages.
    /// </summary>
    public static class ResultMessageFormatter
    {
        /// <summary>
        /// Creates the readable list.
        /// </summary>
        /// <param name="value">The value.</param>
        public static string CreateReadableList(this IList<ResultMessage> value)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (value != null)
            {
                foreach (ResultMessage failureMessage in value.Where(m => m.Success == false))
                {
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(failureMessage.Message);
                }

                foreach (ResultMessage succesMessage in value.Where(m => m.Success))
                {
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.AppendFormat("Exception of type {0} tested succesfully", succesMessage.ExceptionType);
                }
            }

            return stringBuilder.ToString();
        }
    }
}
