using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure.Queries.Interfaces
{
    public interface IConnection<TContext>
    {
        Task<IEnumerable<TEntity>> Query<TEntity>(string sql, object parameters = null);
        Task<int> ExecuteAsync(string sql, object parameters = null);
    }
}
