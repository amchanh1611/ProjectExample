using FluentValidation;
using ProjectExample.Modules.Medias.Entities;

namespace ProjectExample.Modules.Medias.Requests
{
    public class CreateMediaRequest : CreateOrUpdateMediaRequest
    {
        public IFormFile? File { get; set; }
    }
    public class CreateMediaRequestValidator : AbstractValidator<CreateMediaRequest>
    {
        public CreateMediaRequestValidator()
        {
            RuleFor(x => x).SetValidator(new CreateOrUpdateMediaRequestValidator());
            RuleFor(x => x.File).NotEmpty().WithMessage("{property} is required");     
            
        }
    }

}
