using Autofac;

namespace Blog.ViewModels;

public class ViewModelModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterType<WorkLogsViewModel>().InstancePerDependency();
    }
}
