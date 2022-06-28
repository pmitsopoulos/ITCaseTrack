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
        public int? Id { get; set; }

        [CascadingParameter]
        MudDialogInstance mudDialog {get;set;}

        [Parameter]
        public EventCallback OnPost{get;set;}
        [Parameter]
        public EventCallback OnCancel{get;set;}
        private ContactDtoValidator validator = new ContactDtoValidator();

        public ContactDto Contact {get;set;} = new ContactDto();


        private async Task Post()
        {
            await client.PostAsJsonAsync<ContactDto>("https://localhost:7099/api/Contacts", Contact);
            notification.Add("Contact created successfully.", Severity.Success);
            await OnPost.InvokeAsync();
            mudDialog.Close();
        }

        private async Task Cancel()
        {
            OnCancel.InvokeAsync();
            mudDialog.Close();
        }



    }
}