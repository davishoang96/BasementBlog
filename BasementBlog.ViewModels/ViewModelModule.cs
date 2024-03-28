using Autofac;

namespace BasementBlog.ViewModels;

public class ViewModelModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterType<HomeViewModel>().InstancePerDependency();
        builder.RegisterType<PostViewModel>().InstancePerDependency();
        builder.RegisterType<EditPostViewModel>().InstancePerDependency();
        builder.RegisterType<DashboardViewModel>().InstancePerDependency();
    }
}
