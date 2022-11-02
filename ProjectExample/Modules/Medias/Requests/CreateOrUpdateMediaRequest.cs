﻿using FluentValidation;

namespace ProjectExample.Modules.Medias.Requests
{
    public class CreateOrUpdateMediaRequest
    {
        public string? Name { get; set; }

    }
    public class CreateOrUpdateMediaRequestValidator : AbstractValidator<CreateOrUpdateMediaRequest>
    {
        public CreateOrUpdateMediaRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("{propertyname} is required");
        }
    }
}