using Microsoft.AspNetCore.Mvc;
using ProjectExample.Modules.Medias.Entities;
using ProjectExample.Modules.Medias.Requests;
using ProjectExample.Modules.Medias.Response;
using ProjectExample.Modules.Medias.Services;
using ProjectExample.Persistence.PaggingBase;

namespace ProjectExample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchedulesController : ControllerBase
    {
        private readonly IScheduleServices scheduleServices;

        public SchedulesController(IScheduleServices scheduleServices)
        {
            this.scheduleServices = scheduleServices;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateScheduleRequest scheduleRequest)
        {
            bool result = scheduleServices.Create(scheduleRequest);
            if (result)
                return Ok();
            return BadRequest();
        }

        [HttpPut("{scheduleId}")]
        public IActionResult Update([FromRoute] int scheduleId, [FromBody] UpdateScheduleRequest scheduleRequest)
        {
            bool result = scheduleServices.Update(scheduleId, scheduleRequest);
            if (result)
                return Ok();
            return BadRequest();
        }

        [HttpGet("ScheduleInDay")]
        public IActionResult GetScheduleInDay([FromBody] ScheduleInDayRequest date)
        {
            List<ScheduleInDayResponse> scheduleInDay = scheduleServices.GetScheduleInDay(date);
            if (scheduleInDay.Count > 0)
                return Ok(scheduleInDay);
            return BadRequest("No schedule for today");
        }

        [HttpGet("Search")]
        public IActionResult Search([FromQuery] GetScheduleRequest request)
        {
            PaggingResponse<Schedule> result = scheduleServices.GetSchedules(request);
            if (result != null)
                return Ok(result);
            return BadRequest();
        }
    }
}