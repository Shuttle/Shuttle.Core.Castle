using Castle.Windsor;
using NUnit.Framework;
using Shuttle.Core.Container.Tests;

namespace Shuttle.Core.Castle.Tests
{
    [TestFixture]
    public class Fixture : ContainerFixture
    {
        [Test]
        public void Should_be_able_resolve_all_instances()
        {
            var container = new WindsorComponentContainer(new WindsorContainer());

            RegisterCollection(container);
            ResolveCollection(container);
        }

        [Test]
        public void Should_be_able_to_register_and_resolve_a_singleton()
        {
            var container = new WindsorComponentContainer(new WindsorContainer());

            RegisterSingleton(container);
            ResolveSingleton(container);
        }

        [Test]
        public void Should_be_able_to_register_and_resolve_transient_components()
        {
            var container = new WindsorComponentContainer(new WindsorContainer());

            RegisterTransient(container);
            ResolveTransient(container);
        }

		[Test]
		public void Should_be_able_to_register_and_resolve_a_multiple_singleton()
		{
			var container = new WindsorComponentContainer(new WindsorContainer());

			RegisterMultipleSingleton(container);
			ResolveMultipleSingleton(container);
		}

		[Test]
		public void Should_be_able_to_register_and_resolve_multiple_transient_components()
		{
			var container = new WindsorComponentContainer(new WindsorContainer());

			RegisterMultipleTransient(container);
			ResolveMultipleTransient(container);
		}
	}
}