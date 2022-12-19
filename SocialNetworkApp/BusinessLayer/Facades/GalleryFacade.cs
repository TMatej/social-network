using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs.Photo;
using BusinessLayer.Facades.Interfaces;
using DataAccessLayer.Entity;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Facades;

public class GalleryFacade : IGalleryFacade
{
    private readonly IGalleryService galleryService;
    private readonly IPhotoService photoService;
    private readonly IUnitOfWork uow;
    private readonly IMapper mapper;

    public GalleryFacade(IMapper mapper, IGalleryService galleryService, IPhotoService photoService, IUnitOfWork uow)
    {
        this.galleryService = galleryService;
        this.photoService = photoService;
        this.uow = uow;
        this.mapper = mapper;
    }

    public void DeletePhoto(int galleryId, int photoId)
    {
        var gallery = galleryService.GetByIdWithPhotos(galleryId);
        var photo = gallery.Photos.FirstOrDefault(p => p.Id == photoId);
        if (photo == null)
        {
            throw new Exception("Photo not found");
        }
        photoService.Delete(mapper.Map<Photo>(photo));
        uow.Commit();
    }

    public void PostPhoto(int galleryId, PhotoCreateDTO photoCreateDTO)
    {
        if (!photoCreateDTO.File.ContentType.StartsWith("image"))
        {
            throw new Exception("File needs to be of type image");
        }
        galleryService.UploadPhotoToGallery(photoCreateDTO, galleryId);
        uow.Commit();
    }
}
