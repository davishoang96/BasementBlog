using Autofac;

namespace Blog.Services.Modules;

public class ServiceModules : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<MarkdownService>().AsImplementedInterfaces().InstancePerDependency();
        builder.RegisterType<SqidService>().AsImplementedInterfaces().InstancePerDependency();
        builder.RegisterType<FileService>().AsImplementedInterfaces().InstancePerDependency();
    }
}
