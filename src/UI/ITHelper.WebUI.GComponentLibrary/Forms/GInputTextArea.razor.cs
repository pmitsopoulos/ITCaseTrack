using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace ITHelper.WebUI.GComponentLibrary.Forms
{
    public partial class GInputTextArea
    {
        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public string Id { get; set; }
        
        [Parameter]
        public string BindedValue { get; set; }
        [Parameter]
        public string Class { get; set; }
        [Parameter]
        public EventCallback<string> BindedValueChanged { get; set; }

        private async Task UpdateBindedValue()
        {
            await BindedValueChanged.InvokeAsync(BindedValue);
        }
    }
}