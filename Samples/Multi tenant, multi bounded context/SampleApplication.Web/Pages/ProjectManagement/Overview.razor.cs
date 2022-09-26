using Microsoft.AspNetCore.Components;
using SecondContext.Application.Queries.Interfaces;
using SecondContext.Application.Queries.ReadModels;

namespace SampleApplication.Web.Pages.ProjectManagement
{
    public partial class Overview : ComponentBase
    {
        #region Fields

        private ProjectOverviewReadModel _projectOverviewReadModel;

        #endregion

        [Parameter]
        public int TenantId { get; set; }

        [Inject]
        protected ISecondContextQueries SecondContextQueries { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            _projectOverviewReadModel = await SecondContextQueries.GetProjectOverviewAsync(TenantId);
        }
    }
}
