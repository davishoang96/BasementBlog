﻿using Autofac;

namespace Blog.ViewModels;

public class ViewModelModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterType<PostViewModel>().InstancePerDependency();
        builder.RegisterType<EditPostViewModel>().InstancePerDependency();
        builder.RegisterType<DashboardViewModel>().InstancePerDependency();
        builder.RegisterType<WorkLogsViewModel>().InstancePerDependency();
    }
}
