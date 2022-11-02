using FluentValidation;

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
        public CreateOrUpdateScheduleRequestValidator()
        {
            RuleFor(x => x.DateStart).NotEmpty().WithMessage("{propertyname} is required");
            RuleFor(x => x.DateEnd).NotEmpty().WithMessage("{propertyname} is required")
            .GreaterThan(x=>x.DateStart).WithMessage("{propertyname} greater than {ComparisonProperty}");
            RuleFor(x => x.TimeStart).NotEmpty().WithMessage("{propertyname} is required");
            RuleFor(x => x.TimeEnd).NotEmpty().WithMessage("{propertyname} is required")
               .GreaterThan(x => x.TimeStart).WithMessage("{propertyname} greater than {ComparisonProperty}"); ;
            RuleFor(x => x.MediaId).NotEmpty().WithMessage("{propertyname} is required");
                //check media co ton tai k
                //bat loi thoi gian trung
        }
    }
}
