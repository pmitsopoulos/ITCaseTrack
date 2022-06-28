using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITCaseTrack.Application.Contracts.Persistence;
using ITCaseTrack.Application.DTOs.ContactDTOs;
using ITCaseTrack.Application.Features.Common.Handlers.Queries;
using ITCaseTrack.Domain.Entities;

namespace ITCaseTrack.Application.Features.Contacts.Handlers.Queries
{
    public class GetContactsBySearchTermRequestHandler
    : GetBySearchTermRequestHandler<IContactRepository, ContactDto, Contact>
    {
        public GetContactsBySearchTermRequestHandler(IContactRepository repository, IMapper mapper)
         : base(repository, mapper)
        {
        }
    }
}