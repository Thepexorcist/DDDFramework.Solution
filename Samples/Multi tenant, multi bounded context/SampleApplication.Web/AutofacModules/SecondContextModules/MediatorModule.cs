using Autofac;
using Domain.Infrastructure.Messaging.Interfaces;
using MediatR;
using SecondContext.Application.CommandHandlers;
using System.Reflection;

namespace SampleApplication.Web.AutofacModules.SecondContextModules
{
    public class MediatorModule : Autofac.Module
    {
        public IConfiguration Configuration { get; }

        public MediatorModule(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
               .AsImplementedInterfaces();

            // This command will register all commands and add them to autofac.
            builder.RegisterAssemblyTypes(typeof(CreateProjectCommandHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            //builder.RegisterAssemblyTypes(typeof(ProjectRegisteredDomainEventNotification).GetTypeInfo().Assembly)
            //   .AsClosedTypesOf(typeof(IDomainEventNotification<>));

            // This command will register all the domain event handlers.
            //builder.RegisterAssemblyTypes(typeof(TenantActivatedDomainEventHandler).GetTypeInfo().Assembly)
            //    .AsClosedTypesOf(typeof(INotificationHandler<>));

            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });
        }
    }
}
