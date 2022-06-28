using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITCaseTrack.Application.DTOs.AppSystemDTOs;
using ITCaseTrack.Application.DTOs.CaseDTOs;
using ITCaseTrack.Application.DTOs.ContactDTOs;
using ITCaseTrack.Domain.Entities;

namespace ITCaseTrack.Application.Mapping.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AppSystem, AppSystemDto>().ReverseMap();
            CreateMap<Contact, ContactDto>().ReverseMap();
            CreateMap<Case, CaseDto>().ReverseMap();
        }
    }
}