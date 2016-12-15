using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Core.Castle
{
    public class WindsorComponentContainer : IComponentRegistry, IComponentResolver
    {
        private readonly IWindsorContainer _container;

        public WindsorComponentContainer(IWindsorContainer container)
        {
            Guard.AgainstNull(container, "container");

            _container = container;
        }

        public object Resolve(Type serviceType)
        {
            Guard.AgainstNull(serviceType, "serviceType");

            try
            {
                return _container.Resolve(serviceType);
            }
            catch (Exception ex)
            {
                throw new TypeResolutionException(ex.Message, ex);
            }
        }

        public object Resolve(string name, Type serviceType)
        {
            Guard.AgainstNull(serviceType, "serviceType");

            try
            {
                return _container.Resolve(name, serviceType);
            }
            catch (Exception ex)
            {
                throw new TypeResolutionException(ex.Message, ex);
            }
        }

        public IEnumerable<object> ResolveAll(Type serviceType)
        {
            Guard.AgainstNull(serviceType, "serviceType");

            try
            {
                return _container.ResolveAll(serviceType).Cast<object>();
            }
            catch (Exception ex)
            {
                throw new TypeResolutionException(ex.Message, ex);
            }
        }

        public IComponentRegistry Register(Type serviceType, Type implementationType, Lifestyle lifestyle)
        {
            Guard.AgainstNull(serviceType, "serviceType");
            Guard.AgainstNull(implementationType, "implementationType");

            try
            {
                switch (lifestyle)
                {
                    case Lifestyle.Transient:
                        {
                            _container.Register(
                                Component.For(serviceType).ImplementedBy(implementationType).LifestyleTransient());

                            break;
                        }
                    default:
                        {
                            _container.Register(
                                Component.For(serviceType).ImplementedBy(implementationType).LifestyleSingleton());

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

        public IComponentRegistry Register(string name, Type serviceType, Type implementationType, Lifestyle lifestyle)
        {
            Guard.AgainstNullOrEmptyString(name, "name");
            Guard.AgainstNull(serviceType, "serviceType");
            Guard.AgainstNull(implementationType, "implementationType");

            try
            {
                switch (lifestyle)
                {
                    case Lifestyle.Transient:
                        {
                            _container.Register(
                                Component.For(serviceType).ImplementedBy(implementationType).Named(name).LifestyleTransient());

                            break;
                        }
                    default:
                        {
                            _container.Register(
                                Component.For(serviceType).ImplementedBy(implementationType).Named(name).LifestyleSingleton());

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

        public IComponentRegistry Register(Type serviceType, object instance)
        {
            Guard.AgainstNull(serviceType, "serviceType");
            Guard.AgainstNull(instance, "instance");

            try
            {
                _container.Register(Component.For(serviceType).Instance(instance));
            }
            catch (Exception ex)
            {
                throw new TypeRegistrationException(ex.Message, ex);
            }

            return this;
        }
    }
}