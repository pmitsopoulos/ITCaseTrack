using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITCaseTrack.Application.DTOs.CaseDTOs;
using ITCaseTrack.Application.Features.Common.Requests.Commands;
using ITCaseTrack.Application.Features.Common.Requests.Queries;
using ITCaseTrack.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ITCaseTrack.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CasesController : ControllerBase
    {
        private readonly IMediator mediator;

        public CasesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [ActionName("GetAllCases")]
        public async Task<ActionResult<List<CaseDto>>> GetAll ()
        {
            var query = new GetAllRequest<CaseDto>();
            var response = await mediator.Send(query);

            return Ok(response);
        }

        [HttpGet("search/{term?}")]
        public async Task<ActionResult<List<CaseDto>>> GetBySearch (string? term)
        {
            if (string.IsNullOrWhiteSpace(term) || term == string.Empty)
            {
                return RedirectToAction("GetAllCases");
            }
            var query = new GetBySearchTermRequest<CaseDto>() {SearchTerm = term};
            var response = await mediator.Send(query);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CaseDto>> GetById (string id)
        {
            var query = new GetByIdRequest<CaseDto>() {Id = id} ;
            var response = await mediator.Send(query);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<CaseDto>> Create([FromBody] CaseDto _case)
        {
            var command = new CreateRequest<CaseDto>() {Dto = _case};
            try{
                var response = await mediator.Send(command);
                return Ok(response);
            }
            catch
            {
                throw new ApplicationException();
            }
        }

        [HttpPut]
        public async Task Update([FromBody] CaseDto _case)
        {
            var command = new UpdateRequest<CaseDto>() {Dto = _case};
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
            var command = new DeleteRequest<Case>() {Id = id};
          
            await mediator.Send(command);
           
            
        }
    }
}