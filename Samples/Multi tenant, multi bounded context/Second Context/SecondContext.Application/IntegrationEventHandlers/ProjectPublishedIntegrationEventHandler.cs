using Domain.Infrastructure.Messaging.Idempotency;
using Domain.Infrastructure.Tenancy;
using Domain.Tenancy;
using EventBus.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using SecondContext.Application.Commands;
using SecondContext.Application.IntegrationEvents.Incoming;
using SecondContext.Domain.Tenant;
using SecondContext.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondContext.Application.IntegrationEventHandlers
{
    public class ProjectPublishedIntegrationEventHandler : IdempotentMessageHandlerBase<SecondContextDbContext>, IIntegrationEventHandler<ProjectPublishedIntegrationEvent>
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly ILogger<ProjectPublishedIntegrationEventHandler> _logger;

        #endregion

        #region Constructors

        public ProjectPublishedIntegrationEventHandler(IMediator mediator, ILogger<ProjectPublishedIntegrationEventHandler> logger, SecondContextDbContext context) :
            base(context)
        {
            _mediator = mediator;
            _logger = logger;
        }

        #endregion

        public async Task Handle(ProjectPublishedIntegrationEvent integrationEvent)
        {
            // Ensure idempotency.
            var exist = await base.ExistAsync(integrationEvent.Id);

            if (exist)
            {
                _logger.LogInformation("Message with {0} already exists", integrationEvent.Id);
                return;
            }

            var command = new PublishProjectCommand();
            command.TenantId = integrationEvent.TenantId;
            command.ProjectNumber = integrationEvent.UniqueProjectNumber;

            // Add id to processed table.
            await base.CreateIntegrationRequestForCommand(command, integrationEvent.Id);

            // Ensure correct tenant.
            var resolvedTenant = new ResolvedTenant<TenantId>(new TenantId(integrationEvent.TenantId));

            using (TenantContextOverride<TenantId>.Push(resolvedTenant))
            {
                var result = await _mediator.Send(command);

                if (result == false)
                {
                    throw new Exception("Could not process project published integration event");
                }
            }
        }
    }
}
