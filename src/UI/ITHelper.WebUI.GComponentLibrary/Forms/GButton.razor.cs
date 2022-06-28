using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace ITHelper.WebUI.GComponentLibrary.Forms
{
    public partial class GButton
    {
        [Parameter]
        public EventCallback OnClick { get; set; }
        [Parameter]
        public string ButtonName { get; set; }
        [Parameter]
        public string Class { get; set; }
        [Parameter]
        public string MaterialIcon { get; set; }
    }
}