using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITCaseTrack.Application.Contracts.Persistence;
using ITCaseTrack.Application.DTOs.CaseDTOs;
using ITCaseTrack.Application.DTOs.CaseDTOs.Validators;
using ITCaseTrack.Application.Features.Common.Handlers.Commands;
using ITCaseTrack.Domain.Entities;

namespace ITCaseTrack.Application.Features.Cases.Handlers.Commands
{
    public class UpdateCaseRequestHandler : UpdateRequestHandler<ICaseRepository, CaseDto, CaseDtoValidator, Case>
    {
        public UpdateCaseRequestHandler(ICaseRepository repository, IMapper mapper, CaseDtoValidator validator)
         : base(repository, mapper, validator)
        {
        }
    }
}