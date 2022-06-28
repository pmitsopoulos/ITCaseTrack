using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using ITCaseTrack.Application.Contracts.Persistence;

namespace ITCaseTrack.Application.DTOs.CaseDTOs.Validators
{
    public class CaseDtoValidator : AbstractValidator<CaseDto>
    {
        private readonly ICaseRepository? repository;

        public CaseDtoValidator(ICaseRepository repository)
        {
            this.repository = repository;
            ApplyRules();
        }
        public CaseDtoValidator()
        {
            ApplyRules();
        }

        private void ApplyRules()
        {
            // RuleFor(s => s.ApplicationSystem)
            // .NotNull()
            // .WithMessage("A case must have an assigned System.");
            
            RuleFor(d => d.DateIssued)
            .LessThanOrEqualTo(x => x.DueDate)
            .WithMessage("You cannot assign a due date before the Case opening date.");

            RuleFor(d => d.Description)
            .Must(x => !String.IsNullOrEmpty(x))
            .WithMessage("The Description of the case is required.");
            
            RuleFor(at => at.ActionsTaken)
            .Must(x => !String.IsNullOrEmpty(x))
            .When(x => x.Closed == true)
            .WithMessage("When a case is closed the actions that were implemented must be described for future reference.");
            
            RuleFor(at => at.SolutionComments)
            .Must(x => !String.IsNullOrEmpty(x))
            .When(x => x.Closed == true)
            .WithMessage("When a case is closed the solution must be described for future reference.");
        }
    }
}