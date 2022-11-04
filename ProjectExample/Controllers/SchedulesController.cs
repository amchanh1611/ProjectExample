using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectExample.Modules.Medias.Requests;
using ProjectExample.Modules.Medias.Services;

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
    }
}
