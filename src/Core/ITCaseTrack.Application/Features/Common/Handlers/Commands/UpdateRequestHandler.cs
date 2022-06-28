using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using ITCaseTrack.Application.Contracts.Persistence;
using ITCaseTrack.Application.DTOs;
using ITCaseTrack.Application.Features.Common.Requests.Commands;
using MediatR;

namespace ITCaseTrack.Application.Features.Common.Handlers.Commands
{
    public class UpdateRequestHandler<TRepository, TEntityDto, TDtoValidator, TDomainEntity>
    : IRequestHandler<UpdateRequest<TEntityDto>, Unit>
    where TRepository : IGenericRepository<TDomainEntity>
    where TDtoValidator : IValidator<TEntityDto>
    where TDomainEntity : class
    where TEntityDto : BaseDto
    {
        private readonly TRepository repository;
        private readonly IMapper mapper;
        private readonly TDtoValidator validator;

        public UpdateRequestHandler(TRepository repository, IMapper mapper, TDtoValidator validator )
        {
            this.repository = repository;
            this.mapper = mapper;
            this.validator = validator;
        }

        public async Task<Unit> Handle(UpdateRequest<TEntityDto> request, CancellationToken cancellationToken)
        {
            var entityExists = await repository.Exists(request.Dto.Id);

            if(entityExists)
            {
                var modelState = await validator.ValidateAsync(request.Dto);

                if(modelState.IsValid)
                {
                    var tbu = await repository.GetByIdAsync(request.Dto.Id);

                    mapper.Map(request.Dto, tbu);

                    await repository.UpdateAsync(tbu);
                    //await repository.Save();
                }
                else
                {
                    throw new ApplicationException();
                }

                return Unit.Value;
            }
            else
            {
                throw new ApplicationException();
            }

        }
    }
}