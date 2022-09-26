using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure.Messaging.EventualConsistency.Interfaces
{
    public interface IOutboxMessageDbContext
    {
        DbSet<OutboxMessage> OutboxMessages { get; set; }
    }
}
