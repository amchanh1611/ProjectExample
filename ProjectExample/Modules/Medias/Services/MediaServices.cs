using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectExample.Common.Extentions;
using ProjectExample.Modules.Medias.Entities;
using ProjectExample.Modules.Medias.Requests;
using ProjectExample.Modules.Medias.Requests.Override;
using ProjectExample.Modules.Medias.Response;
using ProjectExample.Persistence.PaggingBase;
using ProjectExample.Persistence.Repositories;
using ProjectExample.Persistence.SearchBase;

namespace ProjectExample.Modules.Medias.Services
{
    public interface IMediaServices
    {
        Task<bool> AddAsync(CreateMediaRequest mediaRequest);
        List<ComboboxMedia> ComboboxMedia();
        bool Update(int id, UpdateMediaRequest mediaRequest);
        PaggingResponse<Media> GetMedia(GetMediaRequest request);

    }

    public class MediaServices : IMediaServices
    {
        private readonly IMapper mapper;
        private readonly IRepositoryWrapper repository;

        public MediaServices(IRepositoryWrapper repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<bool> AddAsync(CreateMediaRequest mediaRequest)
        {
            Media media = mapper.Map<Media>(mediaRequest);
            media.File = await Helper.UploadFilesAsync(mediaRequest.FormFile!);
            media.ContentType = mediaRequest.FormFile!.ContentType;
            repository.Media.Create(media);
            return repository.Save();
        }

        public List<ComboboxMedia> ComboboxMedia()
        {
            List<Media> media = repository.Media.FindAll().Distinct().ToList();
            return mapper.Map<List<Media>, List<ComboboxMedia>>(media);
        }

        public PaggingResponse<Media> GetMedia(GetMediaRequest request)
        {
            IQueryable<Media> medias = repository.Media.FindAll().Include(x => x.Schedules);
            if (request.InfoSearch != null)
                medias = SearchingBase<Media>.ApplySearch(medias, request.InfoSearch);
            return PaggingBase<Media>.ApplyPaging(repository.Media.FindAll(), request.CurrentPage, request.PageSize); ;
        }

        public bool Update(int id, UpdateMediaRequest mediaRequest)
        {
            Media media = repository.Media.FindByCondition(x => x.Id == id).FirstOrDefault()!;
            Media mediaUpdate = mapper.Map<UpdateMediaRequest, Media>(mediaRequest);
            repository.Media.Update(mediaUpdate);
            return repository.Save();
        }
    }
}