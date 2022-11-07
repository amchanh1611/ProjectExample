using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectExample.Modules.Medias.Entities;
using ProjectExample.Modules.Medias.Requests;
using ProjectExample.Modules.Medias.Response;
using ProjectExample.Persistence.Contexts;
using ProjectExample.Persistence.Repositories;

namespace ProjectExample.Modules.Medias.Services
{
    public interface IScheduleServices
    {
        bool Create(CreateScheduleRequest scheduleRequest);
        bool Update(int id, UpdateScheduleRequest scheduleRequest);
        List<ScheduleInDayResponse> GetScheduleInDay(ScheduleInDayRequest date);
        SearchOrPagingScheduleResponse Search(SearchOrPagingScheduleRequest request);
    }
    public class ScheduleServices : IScheduleServices
    {
        private readonly IMapper mapper;
        private readonly IRepositoryWrapper repository;
        private readonly ApplicationDbContext context;
        public ScheduleServices(IMapper mapper, IRepositoryWrapper repository, ApplicationDbContext context)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.context = context;
        }
        public bool Create(CreateScheduleRequest scheduleRequest)
        {
            Schedule schedule = mapper.Map<Schedule>(scheduleRequest);
            repository.Schedule.Create(schedule);
            return repository.Save();
        }

        public bool Update(int id, UpdateScheduleRequest scheduleRequest)
        {

            Schedule schedule = repository.Schedule.FindByCondition(x => x.Id == id).FirstOrDefault();
            schedule = mapper.Map<UpdateScheduleRequest,Schedule>(scheduleRequest,schedule);
            repository.Schedule.Update(schedule);
            return repository.Save();
        }

        public List<ScheduleInDayResponse> GetScheduleInDay(ScheduleInDayRequest date)
        {
            List<Schedule> schedule = repository.Schedule.FindByCondition(
                x => x.DateStart <= date.Date && x.DateEnd >= date.Date).ToList();
            return mapper.Map<List<Schedule>,List<ScheduleInDayResponse>>(schedule);
        }

        public SearchOrPagingScheduleResponse Search(SearchOrPagingScheduleRequest request)
        {
            List<Schedule> result = context.schedules.Where(x => x.Description.Contains(request.InfoSearch)).ToList();
            //List<Schedule> result = repository.Schedule.FindByCondition
            //    (
            //        x => x.Description.Contains(request.InfoSearch)
            //    ).ToList();
            return new SearchOrPagingScheduleResponse();
        }
    }
}
