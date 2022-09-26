using FirstContext.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace SampleApplication.Web.Configurations
{
    /// <summary>
    /// Configuration of the database used by the application.
    /// </summary>
    public static class ConfigureFirstContextDbContext
    {
        public static IServiceCollection AddAndConfigureFirstContextDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["FirstContext:ConnectionString"];

            services.AddEntityFrameworkSqlServer().AddDbContext<FirstContextDbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }
    }
}
