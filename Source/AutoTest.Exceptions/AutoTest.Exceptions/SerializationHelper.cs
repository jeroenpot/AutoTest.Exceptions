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
        T SerializeAndDeserializeException<T>(T exceptionToSerialize) where T : Exception;
    }

    internal class SerializationHelper : ISerializationHelper
    {
        /// <summary>
        /// Serializes the and deserialize exception.
        /// </summary>
        /// <typeparam name="T">The type of the exception.</typeparam>
        /// <param name="exceptionToSerialize">The exception to serialize.</param>
        /// <returns>The deserialized exception.</returns>
        public T SerializeAndDeserializeException<T>(T exceptionToSerialize) where T : Exception
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (MemoryStream memoryStream = new MemoryStream())
            {
                T deserializeException = null;
                try
                {
                    formatter.Serialize(memoryStream, exceptionToSerialize);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                }
                catch (Exception exception)
                {
                    ExceptionHelper.ThrowAutoTestException(exceptionToSerialize.GetType(), Properties.Resources.FailedToSerializeException, exception);
                }

                try
                {
                    deserializeException = (T)formatter.Deserialize(memoryStream);
                }
                catch (Exception exception)
                {
                    ExceptionHelper.ThrowAutoTestException(exceptionToSerialize.GetType(), Properties.Resources.FailedToDeserializeException, exception);
                }

                return deserializeException;
            }
        }
    }
}
