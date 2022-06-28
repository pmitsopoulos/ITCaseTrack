using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITCaseTrack.Application.Contracts.Persistence;
using ITCaseTrack.Application.DTOs.AppSystemDTOs;
using ITCaseTrack.Application.Features.Common.Handlers.Queries;
using ITCaseTrack.Domain.Entities;

namespace ITCaseTrack.Application.Features.AppSystems.Handlers.Queries
{
    public class GetSystemsBySearchTermRequestHandler : GetBySearchTermRequestHandler<IAppSystemRepository, AppSystemDto, AppSystem>
    {
        public GetSystemsBySearchTermRequestHandler(IAppSystemRepository repository, IMapper mapper)
         : base(repository, mapper)
        {
        }
    }
}