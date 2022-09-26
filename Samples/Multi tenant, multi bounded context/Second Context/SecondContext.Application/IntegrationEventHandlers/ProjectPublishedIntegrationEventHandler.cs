using EventBus.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using SecondContext.Application.Commands;
using SecondContext.Application.IntegrationEvents.Incoming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondContext.Application.IntegrationEventHandlers
{
    public class ProjectPublishedIntegrationEventHandler : IIntegrationEventHandler<ProjectPublishedIntegrationEvent>
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly ILogger<ProjectPublishedIntegrationEventHandler> _logger;

        #endregion

        #region Constructors

        public ProjectPublishedIntegrationEventHandler(IMediator mediator, ILogger<ProjectPublishedIntegrationEventHandler> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        #endregion

        public async Task Handle(ProjectPublishedIntegrationEvent íntegrationEvent)
        {
            var command = new PublishProjectCommand();
            command.TenantId = íntegrationEvent.TenantId;
            command.ProjectNumber = íntegrationEvent.UniqueProjectNumber;

            var result = await _mediator.Send(command);

            if (result == false)
            {
                throw new Exception("Could not process project published integration event");
            }
        }
    }
}
