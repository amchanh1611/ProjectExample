using FluentValidation;
using ProjectExample.Modules.Medias.Entities;

namespace ProjectExample.Modules.Medias.Requests
{
    public class UpdateMediaRequest : CreateOrUpdateMediaRequest
    {
        public MediaStatus? Status { get; set; }
    }
    public class UpdateMediaRequestValidator : AbstractValidator<UpdateMediaRequest>
    {
        public UpdateMediaRequestValidator()
        {
            RuleFor(x => x).SetValidator(new CreateOrUpdateMediaRequestValidator());
        }
    }
}
