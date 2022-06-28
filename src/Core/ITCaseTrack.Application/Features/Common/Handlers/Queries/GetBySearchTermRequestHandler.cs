using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITCaseTrack.Application.Contracts.Persistence;
using ITCaseTrack.Application.Features.Common.Requests.Queries;
using MediatR;
using AutoMapper;
using ITCaseTrack.Application.Exceptions;

namespace ITCaseTrack.Application.Features.Common.Handlers.Queries
{
    public class GetBySearchTermRequestHandler<TRepository, TEntityDto, TDomainEntity>
    : IRequestHandler<GetBySearchTermRequest<TEntityDto>, List<TEntityDto>>
    where TRepository : IGenericRepository<TDomainEntity>
    where TDomainEntity : class
    {
        private readonly TRepository repository;
        private readonly IMapper mapper;

        public GetBySearchTermRequestHandler(TRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<List<TEntityDto>> Handle(GetBySearchTermRequest<TEntityDto> request, CancellationToken cancellationToken)
        {
            var result = await repository.GetBySearchTermAsync(request.SearchTerm);

            if(result == null)
            {
                throw new NotFoundException(nameof(TDomainEntity), request.SearchTerm);
            }
            return mapper.Map<List<TEntityDto>>(result);
        }
    }
}