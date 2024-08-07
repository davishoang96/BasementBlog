﻿using Autofac;

namespace BasementBlog.Services.Modules;

public class ServiceModules : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<MarkdownService>().AsImplementedInterfaces().InstancePerDependency();
        builder.RegisterType<PostService>().AsImplementedInterfaces().InstancePerDependency();
        builder.RegisterType<BlogDialogService>().AsImplementedInterfaces().InstancePerDependency();
        builder.RegisterType<SqidService>().AsImplementedInterfaces().InstancePerDependency();
        builder.RegisterType<FileService>().AsImplementedInterfaces().InstancePerDependency();
        builder.RegisterType<WorkLogService>().AsImplementedInterfaces().InstancePerDependency();
    }
}
