using FirstContext.Domain.Aggregates.WorkspaceAggregate.DomainServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstContext.Infrastructure
{
    public class UniqueProjectNumberGenerator : IUniqueProjectNumberGenerator
    {
        private static Random random = new Random();

        /// <summary>
        /// This can be any implementation whatsoever.
        /// Inject a repository if database access is needed.
        /// </summary>
        /// <returns></returns>
        public string GenerateProjectNumber()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
