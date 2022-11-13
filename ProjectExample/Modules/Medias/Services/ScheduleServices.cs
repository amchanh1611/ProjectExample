using AutoMapper;
using ProjectExample.Modules.Medias.Entities;
using ProjectExample.Modules.Medias.Requests;
using ProjectExample.Modules.Medias.Response;
using ProjectExample.Persistence.Contexts;
using ProjectExample.Persistence.PaggingBase;
using ProjectExample.Persistence.Repositories;
using ProjectExample.Persistence.SearchBase;
using ProjectExample.Persistence.Sort;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;

namespace ProjectExample.Modules.Medias.Services
{
    public interface IScheduleServices
    {
        bool Create(CreateScheduleRequest scheduleRequest);

        bool Update(int id, UpdateScheduleRequest scheduleRequest);

        List<ScheduleInDayResponse> GetScheduleInDay(ScheduleInDayRequest date);

        PaggingResponse<Schedule> GetSchedules(GetScheduleRequest request);
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
            schedule = mapper.Map<UpdateScheduleRequest, Schedule>(scheduleRequest, schedule);
            repository.Schedule.Update(schedule);
            return repository.Save();
        }

        public List<ScheduleInDayResponse> GetScheduleInDay(ScheduleInDayRequest date)
        {
            List<Schedule> schedule = repository.Schedule.FindByCondition(
                x => x.DateStart <= date.Date && x.DateEnd >= date.Date).ToList();
            return mapper.Map<List<Schedule>, List<ScheduleInDayResponse>>(schedule);
        }

        public PaggingResponse<Schedule> GetSchedules(GetScheduleRequest request)
        {
            IQueryable<Schedule> schedules = repository.Schedule.FindAll();

            if (request.DateFrom != null || request.DateTo != null)
            {
                schedules = repository.Schedule.FindByCondition
                (
                    x => (x.DateStart <= request.DateFrom || (x.DateStart >= request.DateFrom && x.DateStart <= request.DateTo))
                    &&
                    ((x.DateEnd <= request.DateTo && x.DateEnd >= request.DateFrom) || x.DateEnd >= request.DateTo)
                );
            }

            if(request.MediaId != null)
            {
                schedules = repository.Schedule.FindByCondition(x => x.MediaId == request.MediaId);
            }

            if(request.InfoSearch != null)
            {
                schedules = SearchingBase<Schedule>.ApplySearch(schedules, 
                    (x => x.DateStart.ToString().Contains(request.InfoSearch) 
                    || (x.DateEnd.ToString().Contains(request.InfoSearch))));
            }

            schedules = SortingBase<Schedule>.ApplySort(schedules, request.OrderBy);
           
            return PaggingBase<Schedule>.ApplyPaging(schedules, request.CurrentPage, request.PageSize);
        }
        private void ApplySort(ref IQueryable<Schedule> schedules, string orderByQueryString)
        {
            if (!schedules.Any())
                return;

            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                schedules = schedules.OrderBy(x => x.Description);
                return;
            }

            string[] orderParams = orderByQueryString.Trim().Split(',');
            PropertyInfo[] propertyInfos = typeof(Schedule).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            StringBuilder orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                string propertyFromQueryName = param.Split(" ")[0];
                PropertyInfo objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                string sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";

                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {sortingOrder}, ");
            }

            string orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                schedules = schedules.OrderBy(x => x.Description);
                return;
            }

            schedules = schedules.OrderBy(orderQuery);
        }
    }
}