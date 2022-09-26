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
    public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, bool>
    {
        #region Fields

        private readonly ITenantRepository _tenantRepository;

        #endregion

        #region Constructors

        public CreateTenantCommandHandler(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        #endregion

        public async Task<bool> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            var tenantId = await _tenantRepository.GetNextIdentityAsync();

            var tenant = new Tenant(tenantId, request.Name);
            await _tenantRepository.AddAsync(tenant);
            var result = await _tenantRepository.UnitOfWork.SaveAggregateRootAsync(tenant, cancellationToken); ;
            return result;
        }
    }
}
