﻿using FluentValidation;
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
        public CreateOrUpdateScheduleRequestValidator(IRepositoryWrapper repository)
        {
            //FluentValidation
            RuleFor(x => x.DateStart).NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(x => x.DateEnd).NotEmpty().WithMessage("{PropertyName} is required")
            .GreaterThan(x => x.DateStart).WithMessage("{PropertyName} greater than {ComparisonProperty}");

            RuleFor(x => x.TimeStart).NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(x => x.TimeEnd).NotEmpty().WithMessage("{PropertyName} is required")
               .GreaterThan(x => x.TimeStart).WithMessage("{PropertyName} greater than {ComparisonProperty}");

            RuleFor(x => x).Must((request) =>
            {
                return repository.Schedule.FindByCondition(z =>
                    (
                        z.DateStart <= request.DateStart && z.DateEnd >= request.DateStart 
                        || z.DateStart <= request.DateEnd && z.DateEnd >= request.DateEnd
                    )
                    &&
                    (
                        z.TimeStart <= request.TimeStart && z.TimeEnd >= request.TimeStart
                        ||z.TimeStart <= request.TimeEnd && z.TimeEnd >= request.TimeEnd
                    )
                ).Count() == 0;

            }).WithMessage("There is a schedule during this time");

            RuleFor(x => x.MediaId).NotEmpty().WithMessage("{PropertyName} is required")
                .Must((_, media) => 
                {
                    Media? mediaResponse = repository.Media.FindByCondition(x=>x.Id==media).FirstOrDefault();
                    if (mediaResponse != null)
                        return true;
                    return false;
                }).WithMessage("Media does not exist");
        }
    }
}
