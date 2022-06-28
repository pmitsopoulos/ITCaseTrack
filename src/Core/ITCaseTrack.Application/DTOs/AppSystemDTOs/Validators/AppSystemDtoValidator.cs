using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using ITCaseTrack.Application.Contracts.Persistence;

namespace ITCaseTrack.Application.DTOs.AppSystemDTOs.Validators
{
    public class AppSystemDtoValidator : AbstractValidator<AppSystemDto>
    {
        private readonly IAppSystemRepository? repository;

        public AppSystemDtoValidator(IAppSystemRepository repository)
        {
            this.repository = repository;
            ApplyRules();
        }
        public AppSystemDtoValidator()
        {
            ApplyRules();
        }

        private void ApplyRules()
        {
            RuleFor(n => n.Name).Must(x => !String.IsNullOrEmpty(x)).WithMessage("The name of the system is required");
        }
    }
}