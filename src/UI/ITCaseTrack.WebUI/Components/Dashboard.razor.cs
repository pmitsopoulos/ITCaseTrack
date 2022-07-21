using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ITCaseTrack.Application.DTOs.CaseDTOs;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ITCaseTrack.WebUI.Components
{
    public partial class Dashboard
    {
        [Inject]
        HttpClient client {get;set;}
         public string[] labels = {"Pending","Resolved" };
        public double[] data = new double[2];
        private double resolvedCount;
        private double pendingCount;
        public List<ChartSeries> Series = new List<ChartSeries>();
        private string CustomClass="";


        private ChartOptions opts = new ChartOptions()
        {

                ChartPalette = new string[] {"darkred", "#26ffda"}
        };

        private void UpdateChart()
        {
            StateHasChanged();
        }
        protected override async Task OnInitializedAsync()
        {
            var data = await client.GetFromJsonAsync<List<CaseDto>>("https://localhost:7099/api/Cases");
           
             resolvedCount = data.Where(x=>x.Closed==true).Count();

             pendingCount = data.Where(x=>x.Closed==false).Count();

             this.data[0]=pendingCount;
             this.data[1]=resolvedCount;
        }
    }
}