using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using ITCaseTrack.Application.Contracts.Persistence;

namespace ITCaseTrack.Application.DTOs.ContactDTOs.Validators
{
    public class ContactDtoValidator : AbstractValidator<ContactDto>
    {
        private readonly IContactRepository? repository;

        public ContactDtoValidator(IContactRepository repository)
        {
            ApplyRules();
            this.repository = repository;
        }
        public ContactDtoValidator()
        {
            ApplyRules();
        }

        private void ApplyRules()
        {
            RuleFor(x => x.Name).Must(n => !String.IsNullOrEmpty(n)).WithMessage("The Name of the contact is required.");
            RuleFor(x => x.PhoneNumber).Must(n => !String.IsNullOrEmpty(n)).WithMessage("The Phone Number of the contact is required.");
            RuleFor(x => x.Email).Must(n => !String.IsNullOrEmpty(n)).WithMessage("The Email of the contact is required.").EmailAddress().WithMessage("Please provide a valid Email address.");
        }
    }
}