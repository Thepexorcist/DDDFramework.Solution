using FirstContext.Application.Commands;
using FirstContext.Application.Queries.Interfaces;
using FirstContext.Application.Queries.ReadModels;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace SampleApplication.Web.Pages.WorkspaceManagement
{
    public partial class Details : ComponentBase
    {
        #region Fields

        private WorkspaceReadModel _workspace;
        private string _newProjectName = string.Empty;

        #endregion

        [Parameter]
        public int TenantId { get; set; }

        [Parameter]
        public int WorkspaceId { get; set; }

        [Inject]
        protected IFirstContextQueries FirstContextQueries { get; set; }

        [Inject]
        IMediator Mediator { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            _workspace = await FirstContextQueries.GetWorkspaceAsync(TenantId, WorkspaceId);
        }

        private async Task OnActivate()
        {
            if (_workspace.IsActive)
            {
                return;
            }

            var command = new ActivateWorkspaceCommand();
            command.TenantId = TenantId;
            command.WorkspaceId = WorkspaceId;

            await Mediator.Send(command);
            await LoadData();
        }

        private async Task OnDeactivate()
        {
            if (!_workspace.IsActive)
            {
                return;
            }

            var command = new DeactivateWorkspaceCommand();
            command.TenantId = TenantId;
            command.WorkspaceId = WorkspaceId;

            await Mediator.Send(command);
            await LoadData();
        }

        private async Task Publish(ProjectReadModel project)
        {
            if (project.IsPublished)
            {
                return;
            }

            var command = new PublishProjectCommand();
            command.TenantId = _workspace.TenantId;
            command.WorkspaceId = _workspace.WorkspaceId;
            command.ProjectId = project.ProjectId;

            await Mediator.Send(command);
            await LoadData();
        }

        private async Task OnAddNewProjectNameChange(ChangeEventArgs args)
        {
            _newProjectName = args.Value?.ToString();
        }

        private async Task AddNewProjectAsync()
        {
            var command = new RegisterProjectCommand();
            command.TenantId = _workspace.TenantId;
            command.WorkspaceId = _workspace.WorkspaceId;
            command.Name = _newProjectName;

            await Mediator.Send(command);
            await LoadData();
        }
    }
}
