using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using NUnit.Framework;

namespace Shuttle.Core.Castle.Tests
{
    [TestFixture]
    public class DependencyResolverExtensionsFixture
    {
        [Test]
        public void CheckResolver()
        {
            var container = new WindsorContainer();

            Assert.That(container.HasResolver<CollectionResolver>(), Is.False);

            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

            Assert.That(container.HasResolver<CollectionResolver>(), Is.True);
        }
    }
}