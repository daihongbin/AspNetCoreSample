using Autofac;
using DependencyInjectionSample.Services;

namespace DependencyInjectionSample
{
    public class DefaultModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MyDependency>().As<IMyDependency>();
        }
    }
}
