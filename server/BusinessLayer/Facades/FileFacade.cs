using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs.FileEntity;
using BusinessLayer.Facades.Interfaces;

namespace BusinessLayer.Facades;

public class FileFacade : IFileFacade
{
    private readonly IFileService fileService;
    private readonly IMapper mapper;

    public FileFacade(IFileService fileService, IMapper mapper)
    {
        this.fileService = fileService;
        this.mapper = mapper;
    }

    public FileStreamDTO GetFileByGuid(Guid guid)
    {
        return mapper.Map<FileStreamDTO>(fileService.GetByGuid(guid));
    }
} 
