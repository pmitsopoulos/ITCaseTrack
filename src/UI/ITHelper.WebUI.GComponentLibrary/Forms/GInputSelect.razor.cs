using System.Reflection;
using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace ITHelper.WebUI.GComponentLibrary.Forms
{
    public partial class GInputSelect<TData, TValue>
    {
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public List<TData> Data { get; set; } = new List<TData>();

        [Parameter]
        public string? OptionValue { get; set; }

        [Parameter]
        public string OptionDisplay { get; set; }

        [Parameter]
        public string Class { get; set; }
    
        [Parameter]
        public TValue BindedValue { get; set; }

        [Parameter]
        public string Label { get; set; }
        [Parameter]
        public string Icon { get; set; }

        [Parameter]
        public string LabelMaterialIcon { get; set; }
        [Parameter]
        public EventCallback OnAddButtonClick { get; set; }

        [Parameter]
        public EventCallback<TValue> BindedValueChanged { get; set; }
        [Parameter]
        public EventCallback OnClick { get; set; }

        private PropertyInfo property;

        private void DeclareValue(string optVal, TData e)
        {
            this.property = e.GetType().GetProperty(optVal);
        }

        private async Task UpdateBindedValue()
        {
            await BindedValueChanged.InvokeAsync(BindedValue);
        }

    }
}