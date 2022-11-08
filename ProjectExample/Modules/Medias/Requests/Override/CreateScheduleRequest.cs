using FluentValidation;
using ProjectExample.Persistence.Repositories;

namespace ProjectExample.Modules.Medias.Requests
{
    public class CreateScheduleRequest : CreateOrUpdateScheduleRequest
    {
    }
    public class CreateScheduleRequestValidator : AbstractValidator<CreateScheduleRequest>
    {
        public CreateScheduleRequestValidator(IRepositoryWrapper repository)
        {
            RuleFor(x => x).SetValidator(new CreateOrUpdateScheduleRequestValidator(repository));
        }
    }
}
