using AutoMapper;
using ProjectExample.Common.Extentions;
using ProjectExample.Modules.Medias.Entities;
using ProjectExample.Modules.Medias.Requests;
using ProjectExample.Persistence.Repositories;

namespace ProjectExample.Modules.Medias.Services
{
    public interface IMediaService
    {
        Task<bool> AddAsync(CreateMediaRequest mediaRequest);
    }

    public class MediaServices : IMediaService
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
    }
}