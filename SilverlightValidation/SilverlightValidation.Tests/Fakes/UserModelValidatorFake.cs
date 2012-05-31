using System;
using FluentValidation;
using SilverlightValidation.Interfaces;

namespace SilverlightValidation.Tests.Fakes
{
    class UserModelValidatorFake : AbstractValidator<IUserModel>
    {
        private UserModelValidatorFake()
        {
            RuleFor(x => x.Username)
                .Length(3, 8)
                .WithMessage("Must be between 3-8 characters.");

            RuleFor(x => x.Password)
                .Matches(@"^\w*(?=\w*\d)(?=\w*[a-z])(?=\w*[A-Z])\w*$")
                .WithMessage("Must contain lower, upper and numeric chars.");

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("A valid email address is required.");

            RuleFor(x => x.DateOfBirth)
                .Must(BeAValidDateOfBirth)
                .WithMessage("Must be within 100 years of today.");
        }

        private bool BeAValidDateOfBirth(DateTime? dateOfBirth)
        {
            if (dateOfBirth == null) return false;
            if (dateOfBirth.Value > DateTime.Today || dateOfBirth < DateTime.Today.AddYears(-100))
                return false;
            return true;
        }

        public static UserModelValidatorFake Create() { return new UserModelValidatorFake(); }
    }
}
