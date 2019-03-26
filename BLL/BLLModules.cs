using Autofac;

namespace BLL
{
    public class BLLModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(x => x.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(x => x.Name.EndsWith("Wrapper"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
