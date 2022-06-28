using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITCaseTrack.Application.DTOs.ContactDTOs;
using Microsoft.AspNetCore.Components;
namespace ITCaseTrack.WebUI.Components
{
    public partial class ContactView
    {
        [Parameter]
        public ContactDto ContactV{get; set;}
        
    }
}