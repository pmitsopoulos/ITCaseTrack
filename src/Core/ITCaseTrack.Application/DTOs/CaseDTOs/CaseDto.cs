using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITCaseTrack.Application.DTOs.AppSystemDTOs;
using Microsoft.AspNetCore.Components.Forms;
using MongoDB.Bson;

namespace ITCaseTrack.Application.DTOs.CaseDTOs
{
    public class CaseDto : BaseDto
    {
        public string? ApplicationSystemId { get; set; }
        public DateTime DateIssued { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; } = DateTime.Now.AddDays(2);
        public string Description { get; set; }
        public string? ActionsTaken { get; set; }
        public string? SolutionComments { get; set; }
        public bool Closed { get; set; }
        public List<string>? Attachments { get; set; } 
        
    }
}