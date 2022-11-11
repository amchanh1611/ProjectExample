using Microsoft.AspNetCore.Mvc;
using ProjectExample.Modules.Medias.Entities;
using ProjectExample.Modules.Medias.Requests;
using ProjectExample.Modules.Medias.Requests.Override;
using ProjectExample.Modules.Medias.Response;
using ProjectExample.Modules.Medias.Response.Override;
using ProjectExample.Modules.Medias.Services;
using ProjectExample.Persistence.PaggingBase;

namespace ProjectExample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediasController : ControllerBase
    {
        private readonly IMediaServices mediaServices;

        public MediasController(IMediaServices mediaServices)
        {
            this.mediaServices = mediaServices;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateMediaRequest mediaRequest)
        {
            bool result = await mediaServices.AddAsync(mediaRequest);
            if (result)
                return Ok();
            return BadRequest();
        }

        [HttpGet("ComboboxMedia")]
        public IActionResult ComboboxMedia()
        {
            List<ComboboxMedia> result = mediaServices.ComboboxMedia();
            if (result != null)
                return Ok(result);
            return BadRequest();
        }

        [HttpPut("{mediaId}")]
        public IActionResult Update([FromRoute] int mediaId, [FromForm] UpdateMediaRequest mediaRequest)
        {
            bool result = mediaServices.Update(mediaId, mediaRequest);
            if (result)
                return Ok();
            return BadRequest();
        }

        [HttpGet("Search")]
        public IActionResult Search([FromQuery] GetMediaRequest request)
        {
            PaggingResponse<Media> result = mediaServices.Search(request);
            if (result != null)
                return Ok(result);
            return BadRequest();
        }
    }
}