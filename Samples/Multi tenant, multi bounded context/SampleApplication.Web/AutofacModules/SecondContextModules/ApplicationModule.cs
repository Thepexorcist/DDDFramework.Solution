using Autofac;
using Domain.Infrastructure.Messaging.EventualConsistency;
using Domain.Infrastructure.Messaging.Interfaces;
using Domain.Infrastructure.Queries;
using Domain.Infrastructure.Queries.Interfaces;
using Domain.Infrastructure.Tenancy;
using Domain.Infrastructure.Tenancy.Interfaces;
using EventBus.Interfaces;
using SecondContext.Application.IntegrationEventHandlers;
using SecondContext.Application.Queries;
using SecondContext.Application.Queries.Interfaces;
using SecondContext.Domain.Aggregates.ProjectAggregate.Repositories.Interfaces;
using SecondContext.Domain.Employees.Interfaces;
using SecondContext.Domain.Tenant;
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

            builder.RegisterType<DefaultTenantContext<TenantId>>()
              .As<ITenantContext<TenantId>>()
              .InstancePerLifetimeScope();

            builder.RegisterType<SingleTenantContextResolver<TenantId>>()
              .As<ITenantContextResolver<TenantId>>()
              .InstancePerLifetimeScope();

            builder.RegisterType<DefaultTenantContext<int>>()
           .As<ITenantContext<int>>()
           .InstancePerLifetimeScope();

            builder.RegisterType<SingleTenantContextResolver<int>>()
              .As<ITenantContextResolver<int>>()
              .InstancePerLifetimeScope();

            builder.RegisterType<TenantRepositoryFilter<TenantId>>()
              .As<ITenantRepositoryFilter<TenantId>>()
              .InstancePerLifetimeScope();

            builder.RegisterType<TenantQueryFilter<int>>()
              .As<ITenantQueryFilter<int>>()
              .InstancePerLifetimeScope();

            builder.RegisterType<HttpRequestHeaderTenantProvider<int>>()
              .As<ITenantProvider<int>>()
              .WithParameter((pi, c) => pi.Name == "tenantKey", (pi, c) => "Tenant")
              .InstancePerLifetimeScope();

            builder.RegisterType<HttpRequestHeaderTenantProvider<TenantId>>()
              .As<ITenantProvider<TenantId>>()
              .WithParameter((pi, c) => pi.Name == "tenantKey", (pi, c) => "Tenant")
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
