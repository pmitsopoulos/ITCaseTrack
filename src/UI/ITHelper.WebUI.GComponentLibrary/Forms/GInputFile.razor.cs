using System.Reflection;
using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace ITHelper.WebUI.GComponentLibrary.Forms
{
    public partial class GInputFile
    {
       
        [Parameter]
        public string  Class { get; set; }
        [Parameter]
        public string  Id { get; set; }
        [Parameter]
        public string Label { get; set; }

        [CascadingParameter]
        public List<IBrowserFile> BindedValue { get; set; }

        [CascadingParameter]
        public List<string> URLs { get; set; }
       
        private async Task HandleChange(InputFileChangeEventArgs e)
        {
            BindedValue = e.GetMultipleFiles().ToList();
        }

    }
}