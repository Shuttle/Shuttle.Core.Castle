using System;
using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Shuttle.Core.Container;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Castle
{
	public class WindsorComponentContainer : ComponentRegistry, IComponentResolver
	{
		private readonly IWindsorContainer _container;

		public WindsorComponentContainer(IWindsorContainer container)
		{
			Guard.AgainstNull(container, nameof(container));

		    if (!container.HasResolver<CollectionResolver>())
		    {
		        container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));
		    }

		    _container = container;
		}

		public object Resolve(Type dependencyType)
		{
			Guard.AgainstNull(dependencyType, nameof(dependencyType));

			try
			{
				return _container.Resolve(dependencyType);
			}
			catch (Exception ex)
			{
				throw new TypeResolutionException(ex.Message, ex);
			}
		}

		public IEnumerable<object> ResolveAll(Type dependencyType)
		{
			Guard.AgainstNull(dependencyType, nameof(dependencyType));

			try
			{
				return _container.ResolveAll(dependencyType).Cast<object>();
			}
			catch (Exception ex)
			{
				throw new TypeResolutionException(ex.Message, ex);
			}
		}

		public override IComponentRegistry Register(Type dependencyType, Type implementationType, Lifestyle lifestyle)
		{
			Guard.AgainstNull(dependencyType, nameof(dependencyType));
			Guard.AgainstNull(implementationType, nameof(implementationType));

			base.Register(dependencyType, implementationType, lifestyle);

			try
			{
				switch (lifestyle)
				{
					case Lifestyle.Transient:
					{
						_container.Register(
							Component.For(dependencyType)
								.ImplementedBy(implementationType)
								.Named(dependencyType.FullName)
								.LifestyleTransient());

						break;
					}
					default:
					{
						_container.Register(
							Component.For(dependencyType)
								.ImplementedBy(implementationType)
								.Named(dependencyType.FullName)
								.LifestyleSingleton());

						break;
					}
				}
			}
			catch (Exception ex)
			{
				throw new TypeRegistrationException(ex.Message, ex);
			}

			return this;
		}

		public override IComponentRegistry RegisterCollection(Type dependencyType, IEnumerable<Type> implementationTypes,
			Lifestyle lifestyle)
		{
			Guard.AgainstNull(dependencyType, nameof(dependencyType));
			Guard.AgainstNull(implementationTypes, nameof(implementationTypes));

			base.RegisterCollection(dependencyType, implementationTypes, lifestyle);

			try
			{
				switch (lifestyle)
				{
					case Lifestyle.Transient:
					{
						foreach (var implementationType in implementationTypes)
						{
							_container.Register(
								Component.For(dependencyType)
									.ImplementedBy(implementationType)
									.Named($"{dependencyType.FullName}/{implementationType.FullName}")
									.LifestyleTransient());
						}

						break;
					}
					default:
					{
						foreach (var implementationType in implementationTypes)
						{
							_container.Register(
								Component.For(dependencyType)
									.ImplementedBy(implementationType)
									.Named($"{dependencyType.FullName}/{implementationType.FullName}")
									.LifestyleSingleton());
						}

						break;
					}
				}
			}
			catch (Exception ex)
			{
				throw new TypeRegistrationException(ex.Message, ex);
			}

			return this;
		}

		public override IComponentRegistry Register(Type dependencyType, object instance)
		{
			Guard.AgainstNull(dependencyType, nameof(dependencyType));
			Guard.AgainstNull(instance, nameof(instance));

			base.Register(dependencyType, instance);

			try
			{
				_container.Register(Component.For(dependencyType)
					.Instance(instance)
					.Named(dependencyType.FullName));
			}
			catch (Exception ex)
			{
				throw new TypeRegistrationException(ex.Message, ex);
			}

			return this;
		}
	}
}