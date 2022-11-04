using AutoMapper;
using ProjectExample.Modules.Medias.Entities;
using ProjectExample.Modules.Medias.Requests;
using ProjectExample.Persistence.Repositories;

namespace ProjectExample.Modules.Medias.Services
{
    public interface IScheduleServices
    {
        bool Create(CreateScheduleRequest scheduleRequest);
    }
    public class ScheduleServices : IScheduleServices
    {
        private readonly IMapper mapper;
        private readonly IRepositoryWrapper repository;
        public ScheduleServices(IMapper mapper, IRepositoryWrapper repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }
        public bool Create(CreateScheduleRequest scheduleRequest)
        {
            Schedule schedule = mapper.Map<Schedule>(scheduleRequest);
            repository.Schedule.Create(schedule);
            return repository.Save();
        }
    }
}
