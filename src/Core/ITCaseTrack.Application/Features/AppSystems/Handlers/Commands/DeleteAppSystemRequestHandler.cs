using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITCaseTrack.Application.Contracts.Persistence;
using ITCaseTrack.Application.Features.Common.Handlers.Commands;
using ITCaseTrack.Domain.Entities;

namespace ITCaseTrack.Application.Features.AppSystems.Handlers.Commands
{
    public class DeleteAppSystemRequestHandler : DeleteRequestHandler<IAppSystemRepository, AppSystem>
    {
        public DeleteAppSystemRequestHandler(IAppSystemRepository repository) 
        : base(repository)
        {
        }
    }
}