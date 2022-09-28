using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure.Messaging.Idempotency
{
    public class IntegrationRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime ProcessedTime { get; set; }
    }
}
