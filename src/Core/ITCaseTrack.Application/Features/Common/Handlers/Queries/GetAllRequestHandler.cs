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
    public class GetAllRequestHandler<TRepository, TEntityDto, TDomainEntity>
    : IRequestHandler<GetAllRequest<TEntityDto>, List<TEntityDto>>
    where TRepository : IGenericRepository<TDomainEntity>
    where TDomainEntity : class
    {
        private readonly TRepository repository;
        private readonly IMapper mapper;

        public GetAllRequestHandler(TRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<List<TEntityDto>> Handle(GetAllRequest<TEntityDto> request, CancellationToken cancellationToken)
        {
            var result = await repository.GetAllAsync();

            if(result == null)
            {
                throw new NotFoundException(nameof(TDomainEntity),null);
            }
            return mapper.Map<List<TEntityDto>>(result);
        }
    }
}