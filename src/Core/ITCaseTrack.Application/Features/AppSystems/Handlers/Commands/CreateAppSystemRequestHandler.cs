using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITCaseTrack.Application.Contracts.Persistence;
using ITCaseTrack.Application.DTOs.AppSystemDTOs;
using ITCaseTrack.Application.DTOs.AppSystemDTOs.Validators;
using ITCaseTrack.Application.Features.Common.Handlers.Commands;
using ITCaseTrack.Domain.Entities;

namespace ITCaseTrack.Application.Features.AppSystems.Handlers.Commands
{
    public class CreateAppSystemRequestHandler
    : CreateRequestHandler<IAppSystemRepository, AppSystemDto, AppSystemDtoValidator, AppSystem>
    {
        public CreateAppSystemRequestHandler(IAppSystemRepository repository, IMapper mapper, AppSystemDtoValidator validator)
         : base(repository, mapper, validator)
        {
        }
    }
}