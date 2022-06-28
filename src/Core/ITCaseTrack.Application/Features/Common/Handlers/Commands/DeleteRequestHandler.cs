using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITCaseTrack.Application.Contracts.Persistence;
using ITCaseTrack.Application.Exceptions;
using ITCaseTrack.Application.Features.Common.Requests.Commands;
using ITCaseTrack.Domain;
using MediatR;

namespace ITCaseTrack.Application.Features.Common.Handlers.Commands
{
    public class DeleteRequestHandler<TRepository, TDomainEntity>
    : IRequestHandler<DeleteRequest<TDomainEntity>, Unit>
    where TRepository : IGenericRepository<TDomainEntity>
    where TDomainEntity : BaseDomainEntity
    {
        private readonly TRepository repository;
       

        public DeleteRequestHandler(TRepository repository)
        {
            this.repository = repository;
        }
        public async Task<Unit> Handle(DeleteRequest<TDomainEntity> request, CancellationToken cancellationToken)
        {
            var entityExists = await repository.Exists(request.Id);
            
                await repository.DeleteAsync(request.Id);
            if(entityExists)
            {
                //await repository.Save();
            }
            // else
            // {
            //     throw new NotFoundException(nameof(TDomainEntity), request.Id);
            // }

            return Unit.Value;        
        }
    }
}