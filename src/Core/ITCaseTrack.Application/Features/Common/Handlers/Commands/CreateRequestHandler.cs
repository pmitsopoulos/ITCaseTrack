using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using ITCaseTrack.Application.Contracts.Persistence;
using ITCaseTrack.Application.Features.Common.Requests.Commands;
using MediatR;

namespace ITCaseTrack.Application.Features.Common.Handlers.Commands
{
    public class CreateRequestHandler<TRepository, TEntityDto, TDtoValidator, TDomainEntity>
    : IRequestHandler<CreateRequest<TEntityDto>, TEntityDto>
    where TRepository : IGenericRepository<TDomainEntity> 
    where TDtoValidator : IValidator<TEntityDto>
    where TDomainEntity : class
    {
        private readonly TRepository repository;
        private readonly IMapper mapper;
        private readonly IValidator<TEntityDto> validator;

        public CreateRequestHandler(TRepository repository, IMapper mapper, TDtoValidator validator)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.validator = validator;
        }
        public async Task<TEntityDto> Handle(CreateRequest<TEntityDto> request, CancellationToken cancellationToken)
        {
            var modelState = await validator.ValidateAsync(request.Dto);
            if(modelState.IsValid)
            {
                var tbc = mapper.Map<TDomainEntity>(request.Dto);
                var createdEntity = await repository.CreateAsync(tbc);
                return mapper.Map<TEntityDto>(createdEntity);
            }
            else
            {
                throw new ApplicationException();
            }
        }
    }
}