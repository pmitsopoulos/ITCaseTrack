using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITCaseTrack.Application.Contracts.Persistence;
using ITCaseTrack.Application.Exceptions;
using ITCaseTrack.Application.Features.Common.Requests.Queries;
using MediatR;

namespace ITCaseTrack.Application.Features.Common.Handlers.Queries
{
    public class GetByIdRequestHandler<TRepository, TEntityDto, TDomainEntity>
    : IRequestHandler<GetByIdRequest<TEntityDto>, TEntityDto>
    where TRepository: IGenericRepository<TDomainEntity>
    where TDomainEntity: class
    {
        private readonly TRepository repository;
        private readonly IMapper mapper;

        public GetByIdRequestHandler(TRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<TEntityDto> Handle(GetByIdRequest<TEntityDto> request, CancellationToken cancellationToken)
        {
            var result = await repository.GetByIdAsync(request.Id);

            if(result == null)
            {
                throw new NotFoundException(nameof(TDomainEntity), request.Id);
            }
            return mapper.Map<TEntityDto>(result);
        }
    }
}