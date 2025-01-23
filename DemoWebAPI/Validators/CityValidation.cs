using DemoWebAPI.Models;
using FluentValidation;

namespace DemoWebAPI.Validators
{
    public class CityValidation : AbstractValidator<CityModel>
    {
        public CityValidation()
        {
            RuleFor(c => c.CityName).NotNull().NotEmpty().WithMessage("CityName is must be required.")
                .Length(3,20).WithMessage("country name must be between 3 and 20 characters.")
                .Matches("^[A-Za-z ]*$").WithMessage("Country name must contain only letters and spaces.");
            RuleFor(c => c.CityCode).NotNull().NotEmpty().WithMessage("CityCode is must be required.").MaximumLength(4).WithMessage("required only 4 character");
            RuleFor(c => c.CountryID).NotNull().NotEmpty().WithMessage("CountryID is must be required.");
            RuleFor(c => c.StateID).NotNull().NotEmpty().WithMessage("StateID is must be required.");
        }
    }
}
