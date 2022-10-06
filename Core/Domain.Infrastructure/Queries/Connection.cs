using Dapper;
using Domain.Infrastructure.Queries.Interfaces;
using System.Data.SqlClient;

namespace Domain.Infrastructure.Queries
{
    public class Connection<TContext> : IConnection<TContext>
    {
        #region Fields

        private readonly string _connectionString;

        #endregion

        #region Properties



        #endregion

        #region Constructors

        public Connection(string connectionString)
        {
            _connectionString = connectionString;
        }

        #endregion

        #region Public methods

        public async Task<IEnumerable<TEntity>> Query<TEntity>(string sql, object parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<TEntity>(sql, parameters);
                return result;
            }
        }

        public async Task<int> ExecuteAsync(string sql, object parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, parameters);
                return result;
            }
        }

        #endregion
    }
}
