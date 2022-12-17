
using BusinessLayer.DTOs.FileEntity;

namespace BusinessLayer.Facades.Interfaces
{
    public interface IFileFacade
    {
        FileStreamDTO GetFileByGuid(Guid guid);
    }
}
