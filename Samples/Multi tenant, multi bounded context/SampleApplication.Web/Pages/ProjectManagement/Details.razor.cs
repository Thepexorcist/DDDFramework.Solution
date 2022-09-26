using MediatR;
using Microsoft.AspNetCore.Components;
using SecondContext.Application.Commands;
using SecondContext.Application.Queries.Interfaces;
using SecondContext.Application.Queries.ReadModels;

namespace SampleApplication.Web.Pages.ProjectManagement
{
    public partial class Details : ComponentBase
    {
        #region Fields

        private ProjectReadModel _projectReadModel;
        private DocumentReadModel _selectedDocument;
        private string _electricianEmployeeNumber = string.Empty;
        private string _projectManagerEmployeeNumber = string.Empty;
        private string _documentName = string.Empty;

        #endregion

        [Parameter]
        public int TenantId { get; set; }

        [Parameter]
        public int ProjectId { get; set; }

        [Inject]
        protected ISecondContextQueries SecondContextQueries { get; set; }

        [Inject]
        protected IMediator Mediator { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            _projectReadModel = await SecondContextQueries.GetProjectAsync(TenantId, ProjectId);
        }

        private async Task ChangeProjectManagerAsync()
        {
            var command = new AssignProjectManagerCommand();
            command.TenantId = TenantId;
            command.ProjectId = ProjectId;
            command.EmployeeNumber = int.Parse(_projectManagerEmployeeNumber);

            await Mediator.Send(command);
            await LoadData();
        }

        private async Task AssignElectricianAsync()
        {
            var command = new AssignElectricianCommand();
            command.TenantId = TenantId;
            command.ProjectId = ProjectId;
            command.EmployeeNumber = int.Parse(_electricianEmployeeNumber);

            await Mediator.Send(command);
            await LoadData();
        }

        private async Task DocumentSelected(DocumentReadModel document)
        {
            _selectedDocument = await Task.FromResult(document);
        }

        private async Task UpdateDocument(DocumentReadModel document)
        {
            var command = new UpdateDocumentCommand();
            command.TenantId = TenantId;
            command.ProjectId = ProjectId;
            command.DocumentId = document.DocumentId;

            await Mediator.Send(command);
            await LoadData();

            if (_selectedDocument != null)
            {
                _selectedDocument = _projectReadModel.Documents.First(x => x.DocumentId == document.DocumentId);
            }
        }

        private async Task UploadDocumentAsync()
        {
            var command = new AddDocumentCommand();
            command.TenantId = TenantId;
            command.ProjectId= ProjectId;
            command.DocumentId = Guid.NewGuid();
            command.Name = _documentName;
            command.Description = "Some description";
            command.URL = "http://localhost/" + command.DocumentId.ToString();

            await Mediator.Send(command);
            await LoadData();
        }

        private async Task OnElectricianEmployeeNumberChanged(ChangeEventArgs args)
        {
            _electricianEmployeeNumber = await Task.FromResult(args.Value.ToString());
        }

        private async Task OnProjectManagerEmployeeNumberChanged(ChangeEventArgs args)
        {
            _projectManagerEmployeeNumber = await Task.FromResult(args.Value.ToString());
        }

        private async Task OnDocumentNameChanged(ChangeEventArgs args)
        {
            _documentName = await Task.FromResult(args.Value.ToString());
        }
    }
}
