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
            RuleFor(x => x.DateStart).NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(x => x.DateEnd).NotEmpty().WithMessage("{PropertyName} is required")
            .GreaterThan(x => x.DateStart).WithMessage("{PropertyName} greater than {ComparisonProperty}");

            RuleFor(x => x.TimeStart).NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(x => x.TimeEnd).NotEmpty().WithMessage("{PropertyName} is required")
               .GreaterThan(x => x.TimeStart).WithMessage("{PropertyName} greater than {ComparisonProperty}");

            RuleFor(x => x).Must((x) =>
            {
                List<Schedule> listSchedule = repository.Schedule.FindByCondition(
               z => (z.DateStart <= x.DateStart && z.DateEnd >= x.DateStart ||
               z.DateStart <= x.DateEnd && z.DateEnd >= x.DateEnd)).ToList();

                if (listSchedule.Count == 0)
                    return true;

                foreach (Schedule schedule in listSchedule)
                {
                    if (schedule.TimeStart <= x.TimeStart && schedule.TimeEnd >= x.TimeStart ||
                    schedule.TimeStart <= x.TimeEnd && schedule.TimeEnd >= x.TimeEnd)
                        return false;
                }
                return true;
            }).WithMessage("There is a schedule during this time");

            RuleFor(x => x.MediaId).NotEmpty().WithMessage("{PropertyName} is required")
                .Must((_, media) => 
                {
                    Media? mediaResponse = this.repository.Media.FindByCondition(x=>x.Id==media).FirstOrDefault();
                    if (mediaResponse != null)
                        return true;
                    return false;
                }).WithMessage("Media does not exist");
        }
        //private bool CheckDateExist(DateTime? date)
        //{
        //    List<Schedule> listSchedule = repository.Schedule.FindByCondition(
        //        x=>(x.DateStart<=date.Value&&x.DateEnd<=date.Value)).ToList();
            
        //    return true;
        //}
        //private bool CheckTimeExist(TimeSpan? time)
        //{
        //    List<Schedule> listSchedule = repository.Schedule.FindAll().ToList();
        //    foreach (var schedule in listSchedule)
        //    {
        //        if (time > schedule.TimeStart && time < schedule.TimeEnd)
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}
    }
}
