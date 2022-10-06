using Autofac;
using Domain.Infrastructure.Messaging.EventualConsistency;
using Domain.Infrastructure.Messaging.Interfaces;
using Domain.Infrastructure.Queries;
using Domain.Infrastructure.Queries.Interfaces;
using Domain.Infrastructure.Tenancy;
using Domain.Infrastructure.Tenancy.Interfaces;
using FirstContext.Application.Queries;
using FirstContext.Application.Queries.Interfaces;
using FirstContext.Domain.Aggregates.TenantAggregate;
using FirstContext.Domain.Aggregates.TenantAggregate.Repositories.Interfaces;
using FirstContext.Domain.Aggregates.WorkspaceAggregate.DomainServices.Interfaces;
using FirstContext.Domain.Aggregates.WorkspaceAggregate.Repositories.Interfaces;
using FirstContext.Infrastructure;
using FirstContext.Infrastructure.Persistance;

namespace SampleApplication.Web.AutofacModules.FirstContextModules
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
            var connectionString = Configuration["FirstContext:ConnectionString"];

            builder.RegisterType<Connection<FirstContextDbContext>>()
                 .As<IConnection<FirstContextDbContext>>()
                 .WithParameter((pi, c) => pi.Name == "connectionString", (pi, c) => connectionString)
                 .InstancePerLifetimeScope();

            builder.RegisterType<FirstContextQueries>()
               .As<IFirstContextQueries>()
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

            builder.RegisterType<TenantRepository>()
              .As<ITenantRepository>()
              .InstancePerLifetimeScope();

            builder.RegisterType<WorkspaceRepository>()
              .As<IWorkspaceRepository>()
              .InstancePerLifetimeScope();

            builder.RegisterType<DomainEventDispatcher<FirstContextDbContext>>()
             .As<IDomainEventDispatcher<FirstContextDbContext>>()
             .InstancePerLifetimeScope();

            builder.RegisterType<UniqueProjectNumberGenerator>()
              .As<IUniqueProjectNumberGenerator>()
              .InstancePerLifetimeScope();
        }
    }
}
