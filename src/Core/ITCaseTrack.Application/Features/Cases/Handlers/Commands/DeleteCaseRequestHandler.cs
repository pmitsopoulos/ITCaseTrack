using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITCaseTrack.Application.Contracts.Persistence;
using ITCaseTrack.Application.Features.Common.Handlers.Commands;
using ITCaseTrack.Domain.Entities;

namespace ITCaseTrack.Application.Features.Cases.Handlers.Commands
{
    public class DeleteCaseRequestHandler : DeleteRequestHandler<ICaseRepository, Case>
    {
        public DeleteCaseRequestHandler(ICaseRepository repository) 
        : base(repository)
        {
        }
    }
}