using Autofac;

namespace BasementBlog.Services.Modules;

public class ServiceModules : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<MarkdownService>().AsImplementedInterfaces().InstancePerDependency();

        builder.RegisterType<PostService>().AsImplementedInterfaces().InstancePerLifetimeScope();
    }
}
