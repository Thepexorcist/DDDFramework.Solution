﻿using Domain.Tenancy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure.Tenancy.Interfaces
{
    public interface ITenantContextResolver<TTenantIdentity>
    {
        ITenant<TTenantIdentity> ResolveTenant();
    }
}
