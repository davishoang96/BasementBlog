using Autofac;
using Blog.Database.Repositories;

namespace Blog.Database;

public class RepositoryModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<PostRepository>().AsImplementedInterfaces().InstancePerDependency();
        builder.RegisterType<WorkLogRepository>().AsImplementedInterfaces().InstancePerDependency();
    }
}