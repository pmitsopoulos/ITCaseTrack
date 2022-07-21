using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ITCaseTrack.Application.DTOs.ContactDTOs;
using ITCaseTrack.Application.DTOs.ContactDTOs.Validators;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ITCaseTrack.WebUI.Components
{
    public partial class ContactForm
    {
        [Inject]
        HttpClient client {get;set;}
        [Inject]
        ISnackbar notification{get;set;}
        [Parameter]
        public string? Id { get; set; }

        [CascadingParameter]
        MudDialogInstance mudDialog {get;set;}

        [Parameter]
        public EventCallback OnPost{get;set;}
        [Parameter]
        public EventCallback OnCancel{get;set;}
        private ContactDtoValidator validator = new ContactDtoValidator();

        [Parameter]
        public ContactDto Contact {get;set;} = new ContactDto();
        private bool isNewContact=false;

        protected override async Task OnInitializedAsync()
        {
            if(!String.IsNullOrEmpty(Id))
            {
                Contact = await client.GetFromJsonAsync<ContactDto>($"https://localhost:7099/api/Contacts/{Id}");
            }
            else
            {
                isNewContact = true;
            }
        }
        private async Task Post()
        {
            try{
                if(String.IsNullOrEmpty(Contact.Id))
                {
                    await client.PostAsJsonAsync<ContactDto>("https://localhost:7099/api/Contacts", Contact);
                    notification.Add("Contact created successfully.", Severity.Success);
                }
                else
                {
                    await client.PutAsJsonAsync<ContactDto>("https://localhost:7099/api/Contacts", Contact);
                    notification.Add("Contact updated successfully.", Severity.Success);
                }
            }
            catch(Exception ex)
            {
                notification.Add($"Something went wrong during the implementation of your changes. Details: {ex.Message}");
            }
            await OnPost.InvokeAsync();
            if(isNewContact)
            {
                mudDialog.Close();
            }
        }

        private async Task Cancel()
        {
            OnCancel.InvokeAsync();
            mudDialog.Close();
        }



    }
}