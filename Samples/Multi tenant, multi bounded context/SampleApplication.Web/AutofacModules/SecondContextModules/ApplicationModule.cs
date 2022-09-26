using Autofac;
using Domain.Infrastructure.Messaging.EventualConsistency;
using Domain.Infrastructure.Messaging.Interfaces;
using Domain.Infrastructure.Queries;
using Domain.Infrastructure.Queries.Interfaces;
using EventBus.Interfaces;
using SecondContext.Application.IntegrationEventHandlers;
using SecondContext.Application.Queries;
using SecondContext.Application.Queries.Interfaces;
using SecondContext.Domain.Aggregates.ProjectAggregate.Repositories.Interfaces;
using SecondContext.Domain.Employees.Interfaces;
using SecondContext.Infrastructure.Employees.Mock;
using SecondContext.Infrastructure.Persistance;
using System.Reflection;

namespace SampleApplication.Web.AutofacModules.SecondContextModules
{
    public class ApplicationModule : Autofac.Module
    {
        public IConfiguration Configuration { get; }

        public ApplicationModule(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var connectionString = Configuration["SecondContext:ConnectionString"];

            builder.RegisterType<Connection<SecondContextDbContext>>()
                 .As<IConnection<SecondContextDbContext>>()
                 .WithParameter((pi, c) => pi.Name == "connectionString", (pi, c) => connectionString)
                 .InstancePerLifetimeScope();

            builder.RegisterType<SecondContextQueries>()
              .As<ISecondContextQueries>()
              .InstancePerLifetimeScope();

            builder.RegisterType<ProjectRepository>()
              .As<IProjectRepository>()
              .InstancePerLifetimeScope();

            builder.RegisterType<EmployeeService>()
              .As<IEmployeeService>()
              .InstancePerLifetimeScope();

            builder.RegisterType<DomainEventDispatcher<SecondContextDbContext>>()
             .As<IDomainEventDispatcher<SecondContextDbContext>>()
             .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(ProjectRegisteredIntegrationEventHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));
        }
    }
}
