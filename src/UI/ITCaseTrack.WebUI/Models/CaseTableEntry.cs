using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITCaseTrack.Application.DTOs.CaseDTOs;

namespace ITCaseTrack.WebUI.Models
{
    public class CaseTableEntry : TableEntry<CaseDto>
    {
        public string SystemName { get; set; }
    }
}