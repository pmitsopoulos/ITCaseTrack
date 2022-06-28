using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using ITCaseTrack.Application.DTOs.AppSystemDTOs;
using ITCaseTrack.Application.Features.Common.Requests.Queries;
using ITCaseTrack.Application.Features.Common.Requests.Commands;
using ITCaseTrack.Domain.Entities;

namespace ITCaseTrack.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppSystemsController : ControllerBase
    {
        private readonly IMediator mediator;

        public AppSystemsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [ActionName("GetAllSystems")]
        public async Task<ActionResult<List<AppSystemDto>>> GetAll ()
        {
            var query = new GetAllRequest<AppSystemDto>();
            var response = await mediator.Send(query);

            return Ok(response);
        }

        [HttpGet("search/{term?}")]
        public async Task<ActionResult<List<AppSystemDto>>> GetBySearch (string? term)
        {
            if (string.IsNullOrWhiteSpace(term) || term == string.Empty)
            {
                return RedirectToAction("GetAllSystems");
            }
            var query = new GetBySearchTermRequest<AppSystemDto>() {SearchTerm = term};
            var response = await mediator.Send(query);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppSystemDto>> GetById (string id)
        {
            var query = new GetByIdRequest<AppSystemDto>() {Id = id} ;
            var response = await mediator.Send(query);

            return Ok(response);
        }

        [HttpPost]
        public async Task Create([FromBody] AppSystemDto system)
        {
            var command = new CreateRequest<AppSystemDto>() {Dto = system};
            try{
                await mediator.Send(command);
            }
            catch
            {
                throw new ApplicationException();
            }
        }

        [HttpPut]
        public async Task Update([FromBody] AppSystemDto system)
        {
            var command = new UpdateRequest<AppSystemDto>() {Dto = system};
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
            var command = new DeleteRequest<AppSystem>() {Id = id};
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