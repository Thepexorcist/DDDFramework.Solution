using Microsoft.EntityFrameworkCore;
using SecondContext.Infrastructure.Persistance;

namespace SampleApplication.Web.Configurations
{
    /// <summary>
    /// Configuration of the database used by the application.
    /// </summary>
    public static class ConfigureSecondContextDbContext
    {
        public static IServiceCollection AddAndConfigureSecondContextDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["SecondContext:ConnectionString"];

            services.AddEntityFrameworkSqlServer().AddDbContext<SecondContextDbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }
    }
}
