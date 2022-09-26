using Domain.Infrastructure.Repositories.EFCore;
using FirstContext.Domain.Aggregates.TenantAggregate;
using FirstContext.Domain.Aggregates.TenantAggregate.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FirstContext.Infrastructure.Persistance
{
    public class TenantRepository : RepositoryBase<FirstContextDbContext, Tenant, TenantId>, ITenantRepository
    {
        public TenantRepository(FirstContextDbContext context) : base(context)
        {
        }

        public async Task<TenantId> GetNextIdentityAsync()
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT NEXT VALUE FOR [dbo].[TenantSequence];";

                if (command.Connection.State == System.Data.ConnectionState.Closed)
                {
                    await command.Connection.OpenAsync();
                }

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var id = reader.GetInt32(0);
                        return new TenantId(id);
                    }
                }
            }

            return new TenantId(default);
        }
    }
}
