using FluentValidation;
using ProjectExample.Persistence.Repositories;

namespace ProjectExample.Modules.Medias.Requests
{
    public class UpdateScheduleRequest :CreateOrUpdateScheduleRequest
    {

    }
    public class UpdateScheduleRequestValidator : AbstractValidator<UpdateScheduleRequest>
    {
        public UpdateScheduleRequestValidator(IRepositoryWrapper repository)
        {
            RuleFor(x => x).SetValidator(new CreateOrUpdateScheduleRequestValidator(repository));
        }
    }
}
