using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITCaseTrack.Application.Contracts.Persistence;
using ITCaseTrack.Application.DTOs.CaseDTOs;
using ITCaseTrack.Application.Features.Common.Handlers.Queries;
using ITCaseTrack.Domain.Entities;

namespace ITCaseTrack.Application.Features.Cases.Handlers.Queries
{
    public class GetCaseByIdRequestHandler : GetByIdRequestHandler<ICaseRepository, CaseDto, Case>
    {
        public GetCaseByIdRequestHandler(ICaseRepository repository, IMapper mapper) 
        : base(repository, mapper)
        {
        }
    }
}