﻿using System;
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

        public T Resolve<T>() where T : class
        {
            return (T)Resolve(typeof(T));
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

        public IComponentRegistry Register(Type serviceType, object instance)
        {
            Guard.AgainstNull(serviceType, "serviceType");
            Guard.AgainstNull(instance, "instance");

            try
            {
                _container.Register(Component.For(serviceType).Instance(instance));
            }
            catch (ComponentRegistrationException ex)
            {
                throw new TypeRegistrationException(ex.Message, ex);
            }

            return this;
        }
    }
}