using FluentValidation;
using DemoWebAPI.Models;

namespace DemoWebAPI.Validators
{
    public class CountryValidation : AbstractValidator<CountryModel>
    {
        public CountryValidation()
        {
            RuleFor(c => c.CountryName).NotNull().NotEmpty().WithMessage("Country Name Must Be Needed");
            RuleFor(c => c.CountryCode).NotNull().NotEmpty().WithMessage("Country Code Must Be needed");
        }
    }
}
