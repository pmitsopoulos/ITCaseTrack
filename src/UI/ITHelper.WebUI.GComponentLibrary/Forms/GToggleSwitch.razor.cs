using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace ITHelper.WebUI.GComponentLibrary.Forms
{
    public partial class GToggleSwitch
    {
        [Parameter]
        public string Id { get; set; } 
        [Parameter]
        public string Class { get; set; } 

        [Parameter]
        public bool BindedValue { get; set; }
        [Parameter]
        public string ToggleOnMessage { get; set; }
        
        [Parameter]
        public string ToggleOffMessage { get; set; }
        [Parameter]
        public string Label { get; set; }
        [Parameter]
        public EventCallback HandleChange{ get; set; }
        [Parameter]
        public EventCallback<bool> BindedValueChanged { get; set; }

        private async Task UpdateBindedValue()
        {
            await BindedValueChanged.InvokeAsync(BindedValue);
        }
        
    }
}