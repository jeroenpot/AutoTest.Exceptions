using System;
using System.Collections.Generic;
using System.Reflection;

namespace AutoTest.Exceptions
{
    internal interface IExceptionResolver
    {
        IList<Type> GetExceptions(Assembly assembly);
    }

    internal class ExceptionResolver : IExceptionResolver
    {
        public IList<Type> GetExceptions(Assembly assembly)
        {
            Type typeOfException = typeof(Exception);
            IList<Type> types = new List<Type>();

            foreach (Type type in assembly.GetTypes())
            {
                if (typeOfException.IsAssignableFrom(type))
                {
                    types.Add(type);
                }
            }

            return types;
        }
    }
}
