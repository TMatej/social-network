using BusinessLayer.DTOs.Photo;
using DataAccessLayer.Entity;

namespace BusinessLayer.Contracts
{
    public interface IGalleryService : IGenericService<Gallery>
    {
        
        Gallery GetByIdWithPhotos(int id);
        Gallery GetByIdWithProfile(int id);
        Gallery GetByIdDetailed(int id);
    }
}
