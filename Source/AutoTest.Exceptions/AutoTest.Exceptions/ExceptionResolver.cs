using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AutoTest.Exceptions
{
    internal interface IExceptionResolver
    {
        IList<Type> GetExceptions(Assembly assembly, params Type[] exceptionsToIgnore);

        IList<Type> GetExceptions(Assembly assembly, ICollection<Type> exceptionsToIgnore);
    }

    internal class ExceptionResolver : IExceptionResolver
    {
        public IList<Type> GetExceptions(Assembly assembly, params Type[] exceptionsToIgnore)
        {
            ICollection<Type> types = null;
            if (exceptionsToIgnore != null)
            {
                types = exceptionsToIgnore.ToList();
            }

            return GetExceptions(assembly, types);
        }

        public IList<Type> GetExceptions(Assembly assembly, ICollection<Type> exceptionsToIgnore)
        {
            Type typeOfException = typeof(Exception);
            ConcurrentBag<Type> types = new ConcurrentBag<Type>();

            Parallel.ForEach(
                assembly.GetTypes(),
                type =>
                {
                    if (exceptionsToIgnore != null && exceptionsToIgnore.Any() && exceptionsToIgnore.Contains(type))
                        return;

                    if (typeOfException.IsAssignableFrom(type))
                    {
                        types.Add(type);
                    }
                });

            return types.ToList();
        }
    }
}
