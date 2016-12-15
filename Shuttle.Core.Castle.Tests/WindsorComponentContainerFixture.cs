using Castle.Windsor;
using NUnit.Framework;
using Shuttle.Core.ComponentContainer.Tests;

namespace Shuttle.Core.Castle.Tests
{
    [TestFixture]
    public class WindsorComponentContainerFixture : ComponentContainerFixture
    {
        [Test]
        public void Should_be_able_resolve_all_instances()
        {
            var container = new WindsorComponentContainer(new WindsorContainer());

            RegisterMultipleInstances(container);
            ResolveMultipleInstances(container);
        }

        [Test]
        public void Should_be_able_to_register_and_resolve_a_named_singleton()
        {
            var container = new WindsorComponentContainer(new WindsorContainer());

            RegisterNamedSingleton(container);
            ResolveNamedSingleton(container);
        }

        [Test]
        public void Should_be_able_to_register_and_resolve_a_singleton()
        {
            var container = new WindsorComponentContainer(new WindsorContainer());

            RegisterSingleton(container);
            ResolveSingleton(container);
        }

        [Test]
        public void Should_be_able_to_register_and_resolve_named_transient_components()
        {
            var container = new WindsorComponentContainer(new WindsorContainer());

            RegisterNamedTransient(container);
            ResolveNamedTransient(container);
        }

        [Test]
        public void Should_be_able_to_register_and_resolve_transient_components()
        {
            var container = new WindsorComponentContainer(new WindsorContainer());

            RegisterTransient(container);
            ResolveTransient(container);
        }
    }
}