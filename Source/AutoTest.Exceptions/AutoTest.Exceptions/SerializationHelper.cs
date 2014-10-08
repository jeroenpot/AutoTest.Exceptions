using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AutoTest.Exceptions
{
    internal interface ISerializationHelper
    {
        /// <summary>
        /// Serializes the and deserialize exception.
        /// </summary>
        /// <typeparam name="T">The type of the exception.</typeparam>
        /// <param name="exceptionToSerialize">The exception to serialize.</param>
        /// <returns>The deserialized exception.</returns>
        ResultMessage SerializeAndDeserializeException<T>(T exceptionToSerialize) where T : Exception;
    }

    internal class SerializationHelper : ISerializationHelper
    {
        /// <summary>
        /// Serializes the and deserialize exception.
        /// </summary>
        /// <typeparam name="T">The type of the exception.</typeparam>
        /// <param name="exceptionToSerialize">The exception to serialize.</param>
        /// <returns>The deserialized exception.</returns>
        public ResultMessage SerializeAndDeserializeException<T>(T exceptionToSerialize) where T : Exception
        {
            ResultMessage resultMessage = new ResultMessage(exceptionToSerialize);

            BinaryFormatter formatter = new BinaryFormatter();

            using (MemoryStream memoryStream = new MemoryStream())
            {
#pragma warning disable 219
                T deserializeException = null;
#pragma warning restore 219
                try
                {
                    formatter.Serialize(memoryStream, exceptionToSerialize);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                }
                catch (Exception exception)
                {
                    return ResultMessageBuilder.ResultMessageForException(exceptionToSerialize.GetType(), Properties.Resources.FailedToSerializeException, exception);
                }

                try
                {
                    // ReSharper disable once RedundantAssignment
                    deserializeException = (T)formatter.Deserialize(memoryStream);
                }
                catch (Exception exception)
                {
                    return ResultMessageBuilder.ResultMessageForException(exceptionToSerialize.GetType(), Properties.Resources.FailedToDeserializeException, exception);
                }

                return resultMessage;
            }
        }
    }
}
