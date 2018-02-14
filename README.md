# Shuttle.Core.Castle

```
PM> Install-Package Shuttle.Core.Autofac
```

The `WindsorComponentContainer` implements both the `IComponentRegistry` and `IComponentResolver` interfaces.  

~~~c#
var container = new WindsorComponentContainer(new WindsorContainer());
~~~

