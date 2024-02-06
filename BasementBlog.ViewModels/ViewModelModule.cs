using Autofac;

namespace BasementBlog.ViewModels
{
    public class ViewModelModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<HomeViewModel>().InstancePerDependency();
        }
    }
}
