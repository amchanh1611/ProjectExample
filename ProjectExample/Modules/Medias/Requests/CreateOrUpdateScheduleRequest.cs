using FluentValidation;
using ProjectExample.Modules.Medias.Entities;
using ProjectExample.Persistence.Repositories;

namespace ProjectExample.Modules.Medias.Requests
{
    public class CreateOrUpdateScheduleRequest
    {
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public TimeSpan? TimeStart { get; set; }
        public TimeSpan? TimeEnd { get; set; }
        public string? Description { get; set; }
        public int? MediaId { get; set; }
    }
    public class CreateOrUpdateScheduleRequestValidator : AbstractValidator<CreateOrUpdateScheduleRequest>
    {
        private readonly IRepositoryWrapper repository;
        public CreateOrUpdateScheduleRequestValidator(IRepositoryWrapper repository)
        {
            //Inject Repository
            this.repository = repository;

            //FluentValidation
            RuleFor(x => x.DateStart).NotEmpty().WithMessage("{PropertyName} is required")
            .Must(CheckDateExist).WithMessage("There is a schedule during this time");

            RuleFor(x => x.DateEnd).NotEmpty().WithMessage("{PropertyName} is required")
            .GreaterThan(x=>x.DateStart).WithMessage("{PropertyName} greater than {ComparisonProperty}")
            .Must(CheckDateExist).WithMessage("There is a schedule during this time");

            RuleFor(x => x.TimeStart).NotEmpty().WithMessage("{PropertyName} is required")
                .Must(CheckTimeExist).WithMessage("There is a schedule during this time");

            RuleFor(x => x.TimeEnd).NotEmpty().WithMessage("{PropertyName} is required")
               .GreaterThan(x => x.TimeStart).WithMessage("{PropertyName} greater than {ComparisonProperty}")
               .Must(CheckTimeExist).WithMessage("There is a schedule during this time"); 

            RuleFor(x => x.MediaId).NotEmpty().WithMessage("{PropertyName} is required")
                .Must((_, media) => 
                {
                    Media? mediaResponse = this.repository.Media.FindByCondition(x=>x.Id==media).FirstOrDefault();
                    if (mediaResponse != null)
                        return true;
                    return false;
                }).WithMessage("Media does not exist");
        }
        private bool CheckDateExist(DateTime? date)
        {
            List<Schedule> listSchedule = repository.Schedule.FindByCondition(
                x=>(x.DateStart<=date.Value&&x.DateEnd<=date.Value)).ToList();
            
            return true;
        }
        private bool CheckTimeExist(TimeSpan? time)
        {
            List<Schedule> listSchedule = repository.Schedule.FindAll().ToList();
            foreach (var schedule in listSchedule)
            {
                if (time > schedule.TimeStart && time < schedule.TimeEnd)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
