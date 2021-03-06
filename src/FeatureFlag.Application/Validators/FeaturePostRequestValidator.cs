﻿using FeatureFlag.Application.Models;
using FluentValidation;

namespace FeatureFlag.Application.Validators
{
    public class FeaturePostRequestValidator : AbstractValidator<FeaturePostRequest>
    {
        public FeaturePostRequestValidator()
        {
            RuleFor(f => f.Name).NotEmpty()
                .WithMessage("Name can't be empty.");
        }
    }
}
