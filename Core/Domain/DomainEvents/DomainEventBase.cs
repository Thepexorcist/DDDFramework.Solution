﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainEvents
{
    public abstract class DomainEventBase
    {
        public DateTime OccurredOn { get; }
        public DomainEventBase()
        {
            OccurredOn = DateTime.Now;
        }
    }
}
