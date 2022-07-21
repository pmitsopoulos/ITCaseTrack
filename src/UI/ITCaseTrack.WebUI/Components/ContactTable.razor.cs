using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ITCaseTrack.Application.DTOs.ContactDTOs;
using ITCaseTrack.WebUI.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ITCaseTrack.WebUI.Components
{
    public partial class ContactTable
    {
         [Inject]
        HttpClient client {get;set;}
        [Inject]
        ISnackbar notification {get;set;}
        [Inject]
        IDialogService dialog {get;set;}
        [Inject]
        NavigationManager navigationManager{get;set;}
        

        public List<ContactDto> Contacts {get;set;} = new List<ContactDto>();
        public List<TableEntry<ContactDto>> ContactEntries{get;set;} = new List<TableEntry<ContactDto>>();
        private string searchTerm = "";

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Contacts = await client.GetFromJsonAsync<List<ContactDto>>("https://localhost:7099/api/Contacts");
                foreach(var con in Contacts)
                {
                    ContactEntries.Add(new TableEntry<ContactDto>{ Entity = con, Revealed = false  });
                }
            }
            catch(Exception ex)
            {
                notification.Add($"There was a problem retrieving the data from the server. Details: {ex.Message}", Severity.Error);
            }
            StateHasChanged();
        }
        private bool SearchContact(TableEntry<ContactDto> dto, string term)
        {
            if(dto.Entity.Name.ToLower().Contains(term.ToLower()))
            {
                return true;
            }
            return false;
        }
        private bool SearchFilter(TableEntry<ContactDto> element) => SearchContact(element, searchTerm);
        private async Task OnAddClick()
        {
            var options = new DialogOptions()
            {
                CloseButton = true,
                DisableBackdropClick=true,
            };
            dialog.Show<ContactForm>("Creating New System",options);
        }
        private async Task OnEditClick(TableEntry<ContactDto> c)
        {
           c.Revealed = !c.Revealed;
        }
        private async Task OnDeleteClick(TableEntry<ContactDto> c)
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", "Do you really want to delete this contact?");
            parameters.Add("ButtonText", "Delete");
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var d = dialog.Show<Dialog>($"Deleting Contact: {c.Entity.Name}", parameters, options);
            var result = await d.Result;
            if(!result.Cancelled)
            {
                try 
                {
                    await client.DeleteAsync($"https://localhost:7099/api/Contacts/{c.Entity.Id}");
                    notification.Add($"Contact Deleted.",Severity.Warning);
                    ContactEntries.Remove(c);
                }
                catch(Exception ex)
                {
                    notification.Add($"Delete request failed. Error: {ex.Message}", Severity.Error);
                }
            }
        }

    }
}