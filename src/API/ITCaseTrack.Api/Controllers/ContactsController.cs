using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITCaseTrack.Application.DTOs.ContactDTOs;
using ITCaseTrack.Application.Features.Common.Requests.Commands;
using ITCaseTrack.Application.Features.Common.Requests.Queries;
using ITCaseTrack.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ITCaseTrack.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ContactsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [ActionName("GetAllContacts")]
        public async Task<ActionResult<List<ContactDto>>> GetAll ()
        {
            var query = new GetAllRequest<ContactDto>();
            var response = await mediator.Send(query);

            return Ok(response);
        }

        [HttpGet("search/{term?}")]
        public async Task<ActionResult<List<ContactDto>>> GetBySearch (string? term)
        {
            if (string.IsNullOrWhiteSpace(term) || term == string.Empty)
            {
                return RedirectToAction("GetAllContacts");
            }
            var query = new GetBySearchTermRequest<ContactDto>() {SearchTerm = term};
            var response = await mediator.Send(query);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDto>> GetById (string id)
        {
            var query = new GetByIdRequest<ContactDto>() {Id = id} ;
            var response = await mediator.Send(query);

            return Ok(response);
        }

        [HttpPost]
        public async Task Create([FromBody] ContactDto contact)
        {
            var command = new CreateRequest<ContactDto>() {Dto = contact};
            try{
                await mediator.Send(command);
            }
            catch(Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }

        [HttpPut]
        public async Task Update([FromBody] ContactDto contact)
        {
            var command = new UpdateRequest<ContactDto>() {Dto = contact};
            try{
                await mediator.Send(command);
            }
            catch
            {
                throw new ApplicationException();
            }
        }

        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            var command = new DeleteRequest<Contact>() {Id = id};
            try{
                await mediator.Send(command);
            }
            catch
            {
                throw new ApplicationException();
            }
        }
    }
}