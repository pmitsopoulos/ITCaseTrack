using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITCaseTrack.Application.DTOs.AppSystemDTOs;

namespace ITCaseTrack.Application.DTOs.ContactDTOs
{
    public class ContactDto : BaseDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? Description { get; set; }
    }
}