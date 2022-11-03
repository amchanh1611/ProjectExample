using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectExample.Modules.Medias.Requests;
using ProjectExample.Modules.Medias.Services;
using System.Net;

namespace ProjectExample.Controllers
{ 
    [ApiController]
    [Route("api/[controller]")]
    public class MediasController : ControllerBase
    {
        private readonly IMediaService service;
        public MediasController(IMediaService service)
        {
            this.service = service;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateMediaRequest mediaRequest)
        {
            bool result = await service.AddAsync(mediaRequest);
            if (result)
                return Ok();
            return BadRequest("Create failed");
        }
    }
}
