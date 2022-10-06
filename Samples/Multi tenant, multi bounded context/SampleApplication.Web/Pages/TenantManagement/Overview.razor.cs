using Blazored.LocalStorage;
using FirstContext.Application.Commands;
using FirstContext.Application.Queries.Interfaces;
using FirstContext.Application.Queries.ReadModels;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace SampleApplication.Web.Pages.TenantManagement
{
    public partial class Overview : ComponentBase
    {
        private IEnumerable<TenantForListReadModel> _tenants;
        private string _newTenantName = string.Empty;

        [Inject]
        protected IFirstContextQueries FirstContextQueries { get; set; }

        [Inject]
        protected IHttpContextAccessor HttpContextAccessor { get; set; }

        [Inject]
        protected IMediator Mediator { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await LoadData();
        }
        
        private async Task LoadData()
        {
            _tenants = await FirstContextQueries.GetAllTenantsAsync();
        }

        private async Task RegisterTenantAsync()
        {
            var command = new CreateTenantCommand();
            command.Name = _newTenantName;

            await Mediator.Send(command);
            await LoadData();
        }

        private async Task SetActiveTenantAsync(TenantForListReadModel tenant)
        {
            if (HttpContextAccessor.HttpContext.Request.Headers.ContainsKey("Tenant"))
            {
                HttpContextAccessor.HttpContext.Request.Headers.Remove("Tenant");
            }

            HttpContextAccessor.HttpContext.Request.Headers.Add("Tenant", tenant.TenantId.ToString());
        }

        private void OnRegisterNewTenantNameChange(ChangeEventArgs args)
        {
            _newTenantName = args.Value?.ToString();
        }
    }
}
