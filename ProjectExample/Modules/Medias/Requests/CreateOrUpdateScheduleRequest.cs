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
            RuleFor(x => x.DateStart).NotEmpty().WithMessage("{propertyname} is required")
            .Must(CheckDateExist).WithMessage("There is a schedule during this time");

            RuleFor(x => x.DateEnd).NotEmpty().WithMessage("{propertyname} is required")
            .LessThan(x=>x.DateStart).WithMessage("{propertyname} greater than {ComparisonProperty}")
            .Must((_, date) => 
            {
                List<Schedule> listSchedule = repository.Schedule.FindAll().ToList();
                foreach (var schedule in listSchedule)
                {
                    if (date >= schedule.DateStart && date <= schedule.DateEnd)
                    {
                        return false;
                    }
                }
                return true;
            }).WithMessage("There is a schedule during this time");

            RuleFor(x => x.TimeStart).NotEmpty().WithMessage("{propertyname} is required")
                .Must(CheckTimeExist).WithMessage("There is a schedule during this time");

            RuleFor(x => x.TimeEnd).NotEmpty().WithMessage("{propertyname} is required")
               .LessThan(x => x.TimeStart).WithMessage("{propertyname} greater than {ComparisonProperty}")
               .Must((_, time) =>
               {
                   List<Schedule> listSchedule = repository.Schedule.FindAll().ToList();
                   foreach (var schedule in listSchedule)
                   {
                       if (time >= schedule.TimeStart && time <= schedule.TimeEnd)
                       {
                           return false;
                       }
                   }
                   return true;
               }).WithMessage("There is a schedule during this time"); 

            RuleFor(x => x.MediaId).NotEmpty().WithMessage("{propertyname} is required")
                .Must((_, media) => 
                {
                    Media mediaResponse = this.repository.Media.FindByCondition(x=>x.Id.Equals(media)).First();
                    if (mediaResponse != null)
                        return true;
                    return false;
                }).WithMessage("Media does not exist");
                //check media co ton tai k
                //bat loi thoi gian trung
        }
        private bool CheckDateExist(DateTime date)
        {
            List<Schedule> listSchedule = repository.Schedule.FindAll().ToList();
            foreach(var schedule in listSchedule)
            {
                if(date>=schedule.DateStart&&date<=schedule.DateEnd)
                {
                    return false;
                }
            }
            return true;
        }
        private bool CheckTimeExist(TimeSpan time)
        {
            List<Schedule> listSchedule = repository.Schedule.FindAll().ToList();
            foreach (var schedule in listSchedule)
            {
                if (time >= schedule.TimeStart && time <= schedule.TimeEnd)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
