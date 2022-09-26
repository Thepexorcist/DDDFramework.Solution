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
    public class ProjectRegisteredIntegrationEventHandler : IIntegrationEventHandler<ProjectRegisteredIntegrationEvent>
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly ILogger<ProjectRegisteredIntegrationEventHandler> _logger;

        #endregion

        #region Constructors

        public ProjectRegisteredIntegrationEventHandler(IMediator mediator, ILogger<ProjectRegisteredIntegrationEventHandler> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        #endregion

        public async Task Handle(ProjectRegisteredIntegrationEvent íntegrationEvent)
        {
            var command = new CreateProjectCommand();
            command.TenantId = íntegrationEvent.TenantId;
            command.ProjectNumber = íntegrationEvent.UniqueProjectNumber;
            command.DisplayName = íntegrationEvent.ProjectName;

            var result = await _mediator.Send(command);

            if (result == false)
            {
                throw new Exception("Could not process project registered integration event");
            }
        }
    }
}
