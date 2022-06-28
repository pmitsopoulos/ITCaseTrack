using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Threading;
using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ITCaseTrack.Application.DTOs.CaseDTOs;
using ITCaseTrack.Application.DTOs.CaseDTOs.Validators;
using ITCaseTrack.Application.DTOs.AppSystemDTOs;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;
using MongoDB.Bson;
using Microsoft.JSInterop;
using ITCaseTrack.Application.Models;
using System.Text.Json;
using MudBlazor;
using ITCaseTrack.Application.DTOs.ContactDTOs;

namespace ITCaseTrack.WebUI.Components
{
    public partial class CaseForm
    {
        [Inject]
        public IDialogService dialog{get;set;}
        [Inject]
        public NavigationManager nav{get;set;}
        [Inject]
        public ISnackbar notification{get;set;}
        [Inject]
        public HttpClient client {get;set;}
        [Parameter]
        public string? Id { get; set; }
        private CaseDtoValidator validator { get; set; } = new CaseDtoValidator();
        [Parameter]
        public CaseDto Case { get; set; } = new CaseDto();
        
        [CascadingParameter]
        MudDialogInstance mudDialog{get; set;}
        
        private EventCallback OnClose{get; set;}
        private bool IsNewCase = false;
        [Parameter]
        public EventCallback OnFormSubmit{get;set;}
        [Parameter]
        public EventCallback OnFormCancellation{get;set;}
        public EventCallback<CaseDto> CaseUpdated{get;set;}
        public List<string> Files { get; set; } = new List<string>();
        public List<FileToUpload> FilesTBU = new List<FileToUpload>();
        public List<AppSystemDto> AppSystems { get; set; } = new List<AppSystemDto>();



        protected override async Task OnInitializedAsync()
        {
            if(!String.IsNullOrEmpty(Id))
            {
                Case = await client.GetFromJsonAsync<CaseDto>($"https://localhost:7099/api/Cases/{Id}");
                await Retrieve();
            }
            else{
                IsNewCase = true;
            }
            AppSystems = await client.GetFromJsonAsync<List<AppSystemDto>>($"https://localhost:7099/api/AppSystems");
            if(!String.IsNullOrEmpty(Case.ApplicationSystemId))
            {
                var sys = AppSystems.FirstOrDefault(x => x.Id == Case.ApplicationSystemId);
            }
        }
        public async Task UpdateCase()
        {
            await CaseUpdated.InvokeAsync(Case);
        }

        protected async Task Retrieve()
        {
            Files = await client.GetFromJsonAsync<List<string>>($"https://localhost:7099/api/Files/{Case.Id}");
        }

        

       /// <summary>
       /// It posts a CaseDto object to the server, then posts a list of FileToUpload objects to the
       /// server, then retrieves the CaseDto object from the server, then posts the CaseDto object to
       /// the server, then updates the CaseDto object on the server
       /// </summary>
        private async Task Post ()
        {
            HttpResponseMessage response;
            if(String.IsNullOrEmpty(Case.Id))
            {
                response = await client.PostAsJsonAsync<CaseDto>($"https://localhost:7099/api/Cases",Case);
                var stream = await response.Content.ReadAsStreamAsync();
                Case = await JsonSerializer.DeserializeAsync<CaseDto>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            await client.PostAsJsonAsync<List<FileToUpload>>($"https://localhost:7099/api/Files/{Case.Id}",FilesTBU);
            await Retrieve();
            await client.PutAsJsonAsync<CaseDto>($"https://localhost:7099/api/Cases",Case);
            await UpdateCase();
            await OnFormSubmit.InvokeAsync();
            notification.Add("Changes were implemented!", Severity.Success);
            if(IsNewCase)
            {
                nav.NavigateTo("https://localhost:7155",true);
            }
            else{
                await OnFormCancellation.InvokeAsync();
            }
        }

        private async Task Cancel()
        {
            if(IsNewCase)
            {
                mudDialog.Close();
            }
            else
            {
                await OnFormCancellation.InvokeAsync();
            }
        }

        /// <summary>
        /// > It reads the file from the input stream, converts it to a byte array, and then converts
        /// the byte array to a base64 string
        /// </summary>
        /// <param name="InputFileChangeEventArgs">This is the event that is triggered when a file is
        /// added to the input element.</param>
        public async Task HandleChange(InputFileChangeEventArgs e)
        {
            foreach(var file in e.GetMultipleFiles())
            {
                var buffer = new byte[file.Size];
                await file.OpenReadStream(file.Size + 1000).ReadAsync(buffer);
                FilesTBU.Add(
                    new FileToUpload{Name=file.Name, Size = file.Size, FileInString = Convert.ToBase64String(buffer)}
                );
            }
        }

        public async Task OnFileDelete(string fileName)
        {
            
            // var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };
            // var parameters = new DialogParameters();
            // parameters.Add("ContentText", "Do you really want to delete this file?");
            // parameters.Add("ButtonText", "Delete");
            // parameters.Add("Color", Color.Error);
            // var d = dialog.Show<Dialog>($"Deleting Attachment", parameters, options);
            // var result = await d.Result;
            // if(!result.Cancelled)
            // {
                try 
                {
                    await client.DeleteAsync($"https://localhost:7099/api/files/{Case.Id}/{fileName}");
                    notification.Add("File Deleted", Severity.Warning);
                    await Retrieve();
                    StateHasChanged();    
                }
                catch(Exception ex)
                {
                    notification.Add($"Delete request failed. Error: {ex.Message}", Severity.Error);
                }
            // }
        }
      
        private async Task HandleSystemFormSubmit()
        {
            AppSystems = await client.GetFromJsonAsync<List<AppSystemDto>>($"https://localhost:7099/api/AppSystems");
            StateHasChanged();

        }
        private async Task HandleSystemFormCancellation()
        {
            
        }

        public async Task AddNewAppSystem()
        {
            var options = new DialogOptions()
            {
                CloseButton = true,
                DisableBackdropClick=true,
            };
            var parameters = new DialogParameters()
            {
                ["OnPost"] = new EventCallbackFactory().Create(this, new Func<Task>(HandleSystemFormSubmit)),
                ["OnCancel"] = new EventCallbackFactory().Create(this, new Func<Task>(HandleSystemFormCancellation)),
            };

            dialog.Show<AppSystemForm>("Create Application System",parameters, options);
        }

    
      
    }
}