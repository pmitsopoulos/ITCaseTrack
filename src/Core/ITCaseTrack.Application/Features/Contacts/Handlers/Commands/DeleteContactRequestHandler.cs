using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITCaseTrack.Application.Contracts.Persistence;
using ITCaseTrack.Application.Features.Common.Handlers.Commands;
using ITCaseTrack.Domain.Entities;

namespace ITCaseTrack.Application.Features.Contacts.Handlers.Commands
{
    public class DeleteContactRequestHandler : DeleteRequestHandler<IContactRepository, Contact>
    {
        public DeleteContactRequestHandler(IContactRepository repository) 
        : base(repository)
        {
        }
    }
}