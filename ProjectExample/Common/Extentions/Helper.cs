using AutoMapper;
using ProjectExample.Modules.Medias.Entities;
using ProjectExample.Modules.Medias.Requests;
using ProjectExample.Modules.Medias.Response;

namespace ProjectExample.Common.Extentions
{
    public class Helper
    {
        public static async Task<string> UploadFilesAsync(IFormFile file)
        {

            string relativePath = $"Media/{DateTime.UtcNow.Ticks + file.FileName}";

            string absolutePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath);

            using (Stream stream = new FileStream(absolutePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return relativePath;
        }
        public static SearchOrPagingScheduleResponse MapPageRequestToPageInfo(SearchOrPaggingScheduleRequest request, List<Schedule> schedules)
        {
            SearchOrPagingScheduleResponse infoPage = new SearchOrPagingScheduleResponse
            {
                TotalCount = schedules.Count(),
                PageSize = request.PageSize,
                Current = request.CurrentPage,
                TotalPages = schedules.Count() / request.PageSize + schedules.Count() % request.PageSize
                //NextPage = currentPage == schedules.Count() / pageSize + schedules.Count() % pageSize
                //? null : currentPage + 1,
                //PreviousPage = currentPage == 1 ? null : currentPage - 1
            };
            infoPage.NextPage = request.CurrentPage == infoPage.TotalPages ? null : request.CurrentPage + 1;
            infoPage.PreviousPage = request.CurrentPage == 1 ? null : request.CurrentPage - 1;
            infoPage.HasNext = infoPage.NextPage != null ? true : false;
            infoPage.HasPrevious=infoPage.PreviousPage != null ? true : false;
            return infoPage;
        }
    }
}
