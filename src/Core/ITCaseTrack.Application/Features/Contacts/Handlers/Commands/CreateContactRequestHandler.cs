using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITCaseTrack.Application.Contracts.Persistence;
using ITCaseTrack.Application.DTOs.ContactDTOs;
using ITCaseTrack.Application.DTOs.ContactDTOs.Validators;
using ITCaseTrack.Application.Features.Common.Handlers.Commands;
using ITCaseTrack.Domain.Entities;

namespace ITCaseTrack.Application.Features.Contacts.Handlers.Commands
{
    public class CreateContactRequestHandler
     : CreateRequestHandler<IContactRepository, ContactDto, ContactDtoValidator, Contact>
    {
        public CreateContactRequestHandler(IContactRepository repository, IMapper mapper, ContactDtoValidator validator) 
        : base(repository, mapper, validator)
        {
        }
    }
}