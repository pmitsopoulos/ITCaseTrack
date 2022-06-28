using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace ITHelper.WebUI.GComponentLibrary.Forms
{
    public partial class GInputDate
    {
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public string Class { get; set; }
    
        [Parameter]
        public DateTime BindedValue { get; set; }

        [Parameter]
        public EventCallback<DateTime> BindedValueChanged {get;set;}

        [Parameter]
        public string Label { get; set; }

        [CascadingParameter]
        public string ParentCasVal { get; set; }

        private async Task UpdateBindedValue()
        {
            await BindedValueChanged.InvokeAsync(BindedValue);
        }


    }
}