using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ITCaseTrack.Application.DTOs.AppSystemDTOs;
using ITCaseTrack.Application.DTOs.AppSystemDTOs.Validators;
using ITCaseTrack.Application.DTOs.CaseDTOs;
using ITCaseTrack.Application.DTOs.ContactDTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace ITCaseTrack.WebUI.Components
{
    public partial class AppSystemForm
    {
        [Inject]
        IDialogService dialog{get;set;}
        [Inject]
        public ISnackbar notification{get;set;}
        [Inject]
        HttpClient client{get;set;}
        [Parameter]
        public string? Id { get; set; }
        [CascadingParameter]
        MudDialogInstance mudDialog{get; set;}
        private AppSystemDtoValidator validator = new AppSystemDtoValidator(); 


        public EventCallback<ContactDto> ContactUpdated{get;set;}

        [Parameter]
        public EventCallback OnCancel{get;set;}
        [Parameter]
        public EventCallback OnPost{get;set;}

        [Parameter]
        public AppSystemDto AppSystem { get; set; } = new AppSystemDto();

        public List<ContactDto> ContactsList { get; set; } = new List<ContactDto>();

        private bool isNewSystem= false; 

        protected override async Task OnInitializedAsync()
        {
            ContactsList = await client.GetFromJsonAsync<List<ContactDto>>("https://localhost:7099/api/Contacts");
            if(!String.IsNullOrEmpty(Id))
            {
                AppSystem = await client.GetFromJsonAsync<AppSystemDto>($"https://localhost:7099/api/AppSystems/{Id}");
            }
            else
            {
                isNewSystem = true;
            }
        }
        public async Task OnAddClick()
        {
            var options = new DialogOptions()
            {
                CloseButton = true,
                DisableBackdropClick=true,
            };
            var parameters = new DialogParameters()
            {
                ["OnPost"] = new EventCallbackFactory().Create(this, new Func<Task>(HandleContactFormSubmit)),
                ["OnCancel"] = new EventCallbackFactory().Create(this, new Func<Task>(HandleContactFormCancellation)),
            };

            dialog.Show<ContactForm>("Create New Contact", parameters, options);
        }
        

        private async Task Post()
        {
            try{
                if(String.IsNullOrEmpty(AppSystem.Id))
                {
                    await client.PostAsJsonAsync<AppSystemDto>("https://localhost:7099/api/AppSystems", AppSystem);
                    notification.Add("System created successfully.", Severity.Success);
                }
                else
                {
                    await client.PutAsJsonAsync<AppSystemDto>("https://localhost:7099/api/AppSystems", AppSystem);
                    notification.Add("System updated successfully.", Severity.Success);
                }
            }
            catch(Exception ex)
            {
                notification.Add($"Something went wrong during the implementation of your changes. Details: {ex.Message}");
            }
            await OnPost.InvokeAsync();
            if(isNewSystem)
            {
                mudDialog.Close();
            }
        }
        
        
        public async Task Cancel()
        {
            await OnCancel.InvokeAsync();
            mudDialog.Close();
        }

        private delegate Task ContactFormSubmit();

        private async Task HandleContactFormSubmit()
        {
             ContactsList = await client.GetFromJsonAsync<List<ContactDto>>("https://localhost:7099/api/Contacts");
             StateHasChanged();
        }
        private async Task HandleContactFormCancellation()
        {
            
        }

    }
}