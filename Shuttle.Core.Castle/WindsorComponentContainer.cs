using System;
using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Core.Castle
{
    public class WindsorComponentContainer : ComponentRegistry, IComponentResolver
    {
        private readonly IWindsorContainer _container;

        public WindsorComponentContainer(IWindsorContainer container)
        {
            Guard.AgainstNull(container, "container");

            _container = container;
        }

        public override IComponentRegistry Register(Type dependencyType, Type implementationType, Lifestyle lifestyle)
        {
            Guard.AgainstNull(dependencyType, "dependencyType");
            Guard.AgainstNull(implementationType, "implementationType");

	        base.Register(dependencyType, implementationType, lifestyle);

            try
            {
                switch (lifestyle)
                {
                    case Lifestyle.Transient:
                    {
                        _container.Register(
                            Component.For(dependencyType).ImplementedBy(implementationType).LifestyleTransient());

                        break;
                    }
                    default:
                    {
                        _container.Register(
                            Component.For(dependencyType).ImplementedBy(implementationType).LifestyleSingleton());

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
            Guard.AgainstNull(dependencyType, "dependencyType");
            Guard.AgainstNull(implementationTypes, "implementationTypes");

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
                                Component.For(dependencyType).ImplementedBy(implementationType).LifestyleTransient());
                        }

                        break;
                    }
                    default:
                    {
                        foreach (var implementationType in implementationTypes)
                        {
                            _container.Register(
                                Component.For(dependencyType).ImplementedBy(implementationType).LifestyleSingleton());
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
            Guard.AgainstNull(dependencyType, "dependencyType");
            Guard.AgainstNull(instance, "instance");

	        base.Register(dependencyType, instance);

            try
            {
                _container.Register(Component.For(dependencyType).Instance(instance));
            }
            catch (Exception ex)
            {
                throw new TypeRegistrationException(ex.Message, ex);
            }

            return this;
        }

        public object Resolve(Type dependencyType)
        {
            Guard.AgainstNull(dependencyType, "dependencyType");

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
            Guard.AgainstNull(dependencyType, "dependencyType");

            try
            {
                return _container.ResolveAll(dependencyType).Cast<object>();
            }
            catch (Exception ex)
            {
                throw new TypeResolutionException(ex.Message, ex);
            }
        }
    }
}