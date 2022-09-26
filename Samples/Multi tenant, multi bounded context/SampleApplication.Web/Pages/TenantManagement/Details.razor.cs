using FirstContext.Application.Commands;
using FirstContext.Application.Queries.Interfaces;
using FirstContext.Application.Queries.ReadModels;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace SampleApplication.Web.Pages.TenantManagement
{
    public partial class Details : ComponentBase
    {
        #region Fields

        private TenantReadModel _tenant;
        private string _newWorkspaceName = string.Empty;

        #endregion

        [Parameter]
        public int TenantId { get; set; }

        [Inject]
        public IFirstContextQueries FirstContextQueries { get; set; }

        [Inject]
        protected IMediator Mediator { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            _tenant = await FirstContextQueries.GetTenantAsync(TenantId);
        }

        private async Task RegisterWorkspaceAsync()
        {
            var command = new CreateWorkspaceCommand();
            command.TenantId = _tenant.TenantId;
            command.Name = _newWorkspaceName;

            await Mediator.Send(command);
            await LoadData();
        }

        private void OnRegisterNewWorkspaceNameChange(ChangeEventArgs args)
        {
            _newWorkspaceName = args.Value?.ToString();
        }

        private async Task OnActivate()
        {
            if (_tenant.IsActive)
            {
                return;
            }

            var command = new ActivateTenantCommand();
            command.TenantId = _tenant.TenantId;

            await Mediator.Send(command);
            await LoadData();
        }

        private async Task OnDeactivate()
        {
            if (!_tenant.IsActive)
            {
                return;
            }

            var command = new DeactivateTenantCommand();
            command.TenantId = _tenant.TenantId;

            await Mediator.Send(command);
            await LoadData();
        }
    }
}
