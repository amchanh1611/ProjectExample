using FluentValidation;

namespace ProjectExample.Modules.Medias.Requests
{
    public abstract class CreateOrUpdateMediaRequest
    {
        public string? Name { get; set; }

    }
    public class CreateOrUpdateMediaRequestValidator : AbstractValidator<CreateOrUpdateMediaRequest>
    {
        public CreateOrUpdateMediaRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
