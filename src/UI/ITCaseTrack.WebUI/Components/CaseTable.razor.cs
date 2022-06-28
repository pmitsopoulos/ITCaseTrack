using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ITCaseTrack.Application.DTOs.AppSystemDTOs;
using ITCaseTrack.Application.DTOs.CaseDTOs;
using ITCaseTrack.Application.DTOs.ContactDTOs;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ITCaseTrack.WebUI.Components
{
    public partial class CaseTable
    {
        [Inject]
        HttpClient client {get;set;}
        [Inject]
        ISnackbar notification {get;set;}
        [Inject]
        IDialogService dialog {get;set;}

        public List<CaseDto> Cases {get;set;} = new List<CaseDto>();
        List<CaseEntry> CaseEntries = new List<CaseEntry>();
        public List<AppSystemDto> Systems {get;set;} = new List<AppSystemDto>();
        private string searchTerm = "";

        [CascadingParameter]
        public double [] data {get; set;}
        private bool revealed=false;

        [Parameter]
        public EventCallback<double[]> UpdateParentChart { get; set; }
        
        
        protected override async Task OnInitializedAsync()
        {
            try{
                Cases = await client.GetFromJsonAsync<List<CaseDto>>("https://localhost:7099/api/cases");
                Systems = await client.GetFromJsonAsync<List<AppSystemDto>>("https://localhost:7099/api/AppSystems");
            }
            catch(Exception ex)
            {
                notification.Add($"There was a problem retrieving the data from the server. Details: {ex.Message}", Severity.Error);
            }
            foreach(var C in Cases)
            {
                CaseEntries.Add(
                    new CaseEntry{ Case = C , Revealed = false } 
                );
            }
            StateHasChanged();
        }

        private string RetrieveSystem(string systemId)
        {
            var sys = Systems.FirstOrDefault(x => x.Id == systemId);
            return sys.Name;
        }

        private async Task OnAddClick()
        {
            var options = new DialogOptions()
            {
                CloseButton = true,
                DisableBackdropClick=true,
            };
            dialog.Show<CaseForm>("Opening New Case",options);
        }
        private async Task OnContactClick(string systemId)
        {
            var system = Systems.FirstOrDefault(x => x.Id == systemId);
           
                try
                {

                    var Contact = await client.GetFromJsonAsync<ContactDto>($"https://localhost:7099/api/Contacts/{system.ContactId}");

                    
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
        private async Task OnEditClick(CaseEntry c)
        {
           c.Revealed = !c.Revealed;
        }
        private async Task OnDeleteClick(CaseEntry c)
        {

            var parameters = new DialogParameters();
            parameters.Add("ContentText", "Do you really want to delete this case?");
            parameters.Add("ButtonText", "Delete");
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var d = dialog.Show<Dialog>($"Deleting Case: {c.Case.Id}", parameters, options);
            var result = await d.Result;
            if(!result.Cancelled)
            {
                try 
                {
                    await client.DeleteAsync($"https://localhost:7099/api/Cases/{c.Case.Id}");
                    notification.Add($"Case Deleted.",Severity.Warning);
                    CaseEntries.Remove(c);
                    data[1] = CaseEntries.Where(x => x.Case.Closed == true).Count();
                    data[0] = CaseEntries.Where(x => x.Case.Closed == false).Count();
                    await UpdateParentChart.InvokeAsync(data);
                }
                catch(Exception ex)
                {
                    notification.Add($"Delete request failed. Error: {ex.Message}", Severity.Error);
                }
            }
        }
        private TableGroupDefinition<CaseDto> _groupDefinition = new()
        {
            GroupName = "Resolved",
            Indentation = false,
            Expandable = true,
            IsInitiallyExpanded=true,
            Selector = (e) => e.Closed ? "Resolved" : "Pending"
        };

        private async Task HandleFormChanges()
        {
            data[1] = CaseEntries.Where(x => x.Case.Closed == true).Count();
            data[0] = CaseEntries.Where(x => x.Case.Closed == false).Count();
            await UpdateParentChart.InvokeAsync(data);
            StateHasChanged();
        }

        class CaseEntry
        {
            public bool Revealed { get; set; }
            public CaseDto Case { get; set; }
            public string SystemName{get;set;}
        }
        private bool SearchCase(CaseEntry dto, string term)
        {

            // if(String.IsNullOrEmpty(term))
            // {
            //     FilteredCaseEntries = CaseEntries;
            //     return true;
            // }
            // else{
            //     FilteredCaseEntries = CaseEntries.Where(x => x.SystemName.ToLower().Contains(term)
            //     || x.Case.Description.ToLower().Contains(term)).ToList();
            //     return true;
            // }
            
            if(dto.Case.Description.ToLower().Contains(term.ToLower()))
            {
                return true;
            }
            return false;
        }
        private bool SearchFilter(CaseEntry element) => SearchCase(element, searchTerm);

    }
}