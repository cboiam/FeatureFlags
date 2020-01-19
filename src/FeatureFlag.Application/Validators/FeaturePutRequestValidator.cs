using FeatureFlag.Application.Models;
using FluentValidation;

namespace FeatureFlag.Application.Validators
{
    public class FeaturePutRequestValidator : AbstractValidator<FeaturePutRequest>
    {
        public FeaturePutRequestValidator()
        {
            RuleFor(f => f.Id).GreaterThan(0)
                .WithMessage("Invalid Id");

            RuleFor(f => f.Name).NotEmpty()
                .WithMessage("Name can't be empty.");

            RuleFor(f => f.Environment).NotEmpty()
                .WithMessage("Environment can't be empty");
        }
    }
}
