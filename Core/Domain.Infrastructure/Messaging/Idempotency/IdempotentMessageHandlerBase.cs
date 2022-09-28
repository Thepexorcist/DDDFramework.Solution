using Domain.Infrastructure.Messaging.Idempotency.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure.Messaging.Idempotency
{
    public abstract class IdempotentMessageHandlerBase<TContext> where TContext : DbContext, IIdempotentDbContext
    {
        #region Fields

        private readonly TContext _context;

        #endregion

        #region Constructors

        protected IdempotentMessageHandlerBase(TContext context)
        {
            _context = context;
        }

        #endregion

        #region Protected methods

        protected async Task CreateIntegrationRequestForCommand<TCommand>(TCommand command, Guid integrationEventId)
        {
            var exist = await ExistAsync(integrationEventId);

            if (exist)
            {
                throw new ArgumentException(String.Format("Request with {0} already exist.", integrationEventId));
            }

            var integrationRequest = new IntegrationRequest();
            integrationRequest.Id = integrationEventId;
            integrationRequest.Name = typeof(TCommand).Name;
            integrationRequest.ProcessedTime = DateTime.Now;

            await _context.IntegrationRequests.AddAsync(integrationRequest);
            await _context.SaveChangesAsync();
        }

        protected async Task<bool> ExistAsync(Guid id)
        {
            var result = await _context.FindAsync<IntegrationRequest>(id);
            return result != null;
        }

        #endregion
    }
}
