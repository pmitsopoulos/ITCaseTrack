using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ITCaseTrack.Application.DTOs.AppSystemDTOs;
using ITCaseTrack.Application.DTOs.ContactDTOs;
using ITCaseTrack.WebUI.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ITCaseTrack.WebUI.Components
{
    public partial class AppSystemTable
    {
        [Inject]
        HttpClient client {get;set;}
        [Inject]
        ISnackbar notification {get;set;}
        [Inject]
        IDialogService dialog {get;set;}
        

        public List<AppSystemDto> Systems {get;set;} = new List<AppSystemDto>();
        public List<TableEntry<AppSystemDto>> SystemEntries{get;set;} = new List<TableEntry<AppSystemDto>>();
        private string searchTerm = "";

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Systems = await client.GetFromJsonAsync<List<AppSystemDto>>("https://localhost:7099/api/AppSystems");
                foreach(var sys in Systems)
                {
                    SystemEntries.Add(new TableEntry<AppSystemDto>{ Entity = sys, Revealed = false  });
                }
            }
            catch(Exception ex)
            {
                notification.Add($"There was a problem retrieving the data from the server. Details: {ex.Message}", Severity.Error);
            }
            StateHasChanged();
        }
        private bool SearchSystem(TableEntry<AppSystemDto> dto, string term)
        {
            if(dto.Entity.Name.ToLower().Contains(term.ToLower()))
            {
                return true;
            }
            return false;
        }
         private bool SearchFilter(TableEntry<AppSystemDto> element) => SearchSystem(element, searchTerm);
        private async Task OnAddClick()
        {
            var options = new DialogOptions()
            {
                CloseButton = true,
                DisableBackdropClick=true,
            };
            dialog.Show<AppSystemForm>("Creating New System",options);
        }
        private async Task OnEditClick(TableEntry<AppSystemDto> c)
        {
           c.Revealed = !c.Revealed;
        }
        private async Task OnDeleteClick(TableEntry<AppSystemDto> c)
        {

            var parameters = new DialogParameters();
            parameters.Add("ContentText", "Do you really want to delete this system?");
            parameters.Add("ButtonText", "Delete");
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var d = dialog.Show<Dialog>($"Deleting System: {c.Entity.Name}", parameters, options);
            var result = await d.Result;
            if(!result.Cancelled)
            {
                try 
                {
                    await client.DeleteAsync($"https://localhost:7099/api/AppSystems/{c.Entity.Id}");
                    notification.Add($"System Deleted.",Severity.Warning);
                    SystemEntries.Remove(c);
                }
                catch(Exception ex)
                {
                    notification.Add($"Delete request failed. Error: {ex.Message}", Severity.Error);
                }
            }
        }

        private async Task OnContactClick(string contactId)
        {  
                try
                {

                    var Contact = await client.GetFromJsonAsync<ContactDto>($"https://localhost:7099/api/Contacts/{contactId}");
                    
                        var options = new DialogOptions()
                        {
                            FullWidth=true,
                            CloseButton = true,
                            DisableBackdropClick = true,
                            CloseOnEscapeKey = true
                        };
                        var parameters = new DialogParameters(){
                            ["ContactV"] = Contact
                        };
                        dialog.Show<ContactView>("Contact Details", parameters, options);
                }
                catch{
                    notification.Add("No Contact was found for this system",Severity.Warning);
                }   
        }
    }
}