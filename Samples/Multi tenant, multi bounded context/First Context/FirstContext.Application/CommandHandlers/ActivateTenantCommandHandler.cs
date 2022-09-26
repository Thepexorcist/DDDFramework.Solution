using FirstContext.Application.Commands;
using FirstContext.Domain.Aggregates.TenantAggregate;
using FirstContext.Domain.Aggregates.TenantAggregate.Repositories.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstContext.Application.CommandHandlers
{
    public class ActivateTenantCommandHandler : IRequestHandler<ActivateTenantCommand, bool>
    {
        #region Fields

        private readonly ITenantRepository _tenantRepository;

        #endregion

        #region Constructors

        public ActivateTenantCommandHandler(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        #endregion

        public async Task<bool> Handle(ActivateTenantCommand request, CancellationToken cancellationToken)
        {
            var tenantId = new TenantId(request.TenantId);

            var tenant = await _tenantRepository.GetAsync(tenantId);
            tenant.Activate();
            var result = await _tenantRepository.UnitOfWork.SaveAggregateRootAsync(tenant, cancellationToken); ;
            return result;
        }
    }
}
