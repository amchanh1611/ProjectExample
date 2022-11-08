using AutoMapper;
using ProjectExample.Common.Extentions;
using ProjectExample.Modules.Medias.Entities;
using ProjectExample.Modules.Medias.Requests;
using ProjectExample.Modules.Medias.Requests.Override;
using ProjectExample.Modules.Medias.Response;
using ProjectExample.Modules.Medias.Response.Override;
using ProjectExample.Persistence.PaggingBase;
using ProjectExample.Persistence.Repositories;

namespace ProjectExample.Modules.Medias.Services
{
    public interface IMediaServices
    {
        Task<bool> AddAsync(CreateMediaRequest mediaRequest);
        List<ComboboxMedia> ComboboxMedia();
        bool Update(int id, UpdateMediaRequest mediaRequest);
        SearchOrPaggingMediaResponse Search(SearchOrPaggingMediaRequest request);
        
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
            media.File = await Helper.UploadFilesAsync(mediaRequest.FormFile);
            media.ContentType = mediaRequest.FormFile.ContentType;
            repository.Media.Create(media);
            return repository.Save();
        }

        public List<ComboboxMedia> ComboboxMedia()
        {
            List<Media> media = repository.Media.FindAll().Distinct().ToList();
            return mapper.Map<List<Media>,List<ComboboxMedia>>(media);
        }

        public SearchOrPaggingMediaResponse Search(SearchOrPaggingMediaRequest request)
        {
            PaggingBase<Media> medias = PaggingBase<Media>.ToPagedList(repository.Media.FindAll(), request.CurrentPage, request.PageSize);

            SearchOrPaggingMediaResponse response = mapper.Map<SearchOrPaggingMediaResponse>(medias);
            response.Medias = medias;
            return response;
        }

        public bool Update(int id, UpdateMediaRequest mediaRequest)
        {
            Media media = repository.Media.FindByCondition(x=>x.Id==id).FirstOrDefault();
            Media mediaUpdate = mapper.Map<UpdateMediaRequest,Media>(mediaRequest);
            repository.Media.Update(mediaUpdate);
            return repository.Save();
        }
    }
}