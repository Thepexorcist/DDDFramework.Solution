using Domain.Infrastructure.Messaging.Idempotency;
using EventBus.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using SecondContext.Application.Commands;
using SecondContext.Application.IntegrationEvents.Incoming;
using SecondContext.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondContext.Application.IntegrationEventHandlers
{
    public class ProjectRegisteredIntegrationEventHandler : IdempotentMessageHandlerBase<SecondContextDbContext>, IIntegrationEventHandler<ProjectRegisteredIntegrationEvent>
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly ILogger<ProjectRegisteredIntegrationEventHandler> _logger;

        #endregion

        #region Constructors

        public ProjectRegisteredIntegrationEventHandler(IMediator mediator, ILogger<ProjectRegisteredIntegrationEventHandler> logger, SecondContextDbContext context) :
            base(context)
        {
            _mediator = mediator;
            _logger = logger;
        }

        #endregion

        public async Task Handle(ProjectRegisteredIntegrationEvent integrationEvent)
        {
            // Ensure idempotency.
            var exist = await base.ExistAsync(integrationEvent.Id);

            if (exist)
            {
                _logger.LogInformation("Message with {0} already exists", integrationEvent.Id);
                return;
            }

            var command = new CreateProjectCommand();
            command.TenantId = integrationEvent.TenantId;
            command.ProjectNumber = integrationEvent.UniqueProjectNumber;
            command.DisplayName = integrationEvent.ProjectName;

            // Add id to processed table.
            await base.CreateIntegrationRequestForCommand(command, integrationEvent.Id);

            var result = await _mediator.Send(command);

            if (result == false)
            {
                throw new Exception("Could not process project registered integration event");
            }
        }
    }
}
