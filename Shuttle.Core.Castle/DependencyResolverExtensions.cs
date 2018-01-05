using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.MicroKernel;
using Castle.Windsor;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Castle
{
    public static class DependencyResolverExtensions
    {
        public static bool HasResolver<T>(this IWindsorContainer container) where T : ISubDependencyResolver
        {
            Guard.AgainstNull(container, nameof(container));

            var resolver = container.Kernel.Resolver;
            var fieldInfo = resolver.GetType().GetField("subResolvers", BindingFlags.NonPublic | BindingFlags.Instance);

            if (fieldInfo == null)
            {
                throw new InvalidOperationException(Resources.SubResolversException);
            }

            return ((IList<ISubDependencyResolver>)fieldInfo.GetValue(resolver)).OfType<T>().Any();
        }
    }
}