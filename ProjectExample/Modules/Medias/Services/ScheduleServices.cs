using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectExample.Common.Extentions;
using ProjectExample.Modules.Medias.Entities;
using ProjectExample.Modules.Medias.Requests;
using ProjectExample.Modules.Medias.Response;
using ProjectExample.Persistence.Contexts;
using ProjectExample.Persistence.PaggingBase;
using ProjectExample.Persistence.Repositories;

namespace ProjectExample.Modules.Medias.Services
{
    public interface IScheduleServices
    {
        bool Create(CreateScheduleRequest scheduleRequest);
        bool Update(int id, UpdateScheduleRequest scheduleRequest);
        List<ScheduleInDayResponse> GetScheduleInDay(ScheduleInDayRequest date);
        SearchOrPagingScheduleResponse Search(SearchOrPaggingScheduleRequest request);
        SearchOrPagingScheduleResponse Pagging(SearchOrPaggingScheduleRequest request);
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

        public SearchOrPagingScheduleResponse Search(SearchOrPaggingScheduleRequest request)
        {
            PaggingBase<Schedule> schedules = PaggingBase<Schedule>.ToPagedList(repository.Schedule.FindAll(), request.CurrentPage, request.PageSize);
             
            SearchOrPagingScheduleResponse response = mapper.Map<SearchOrPagingScheduleResponse>(schedules);

            response.Schedules = schedules;

            return response;
        }

        public SearchOrPagingScheduleResponse Pagging(SearchOrPaggingScheduleRequest request)
        {
            List<Schedule> schedules = repository.Schedule.FindAll().ToList();
            List<Schedule> shedulePagging = schedules.OrderBy(x => x.Description).Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();

            SearchOrPagingScheduleResponse response = Helper.MapPageRequestToPageInfo(request, schedules);

           
            return response;
        }
    }
}
