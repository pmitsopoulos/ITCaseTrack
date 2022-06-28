using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITCaseTrack.Application.DTOs.ContactDTOs;

namespace ITCaseTrack.Application.DTOs.AppSystemDTOs
{
    public class AppSystemDto : BaseDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ContactId { get; set; }
    }
}