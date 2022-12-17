using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Facades.Interfaces;

namespace PresentationLayer.Controllers;

[ApiController]
[Route("[controller]")]
public class FilesController : Controller
{
    private readonly IFileFacade fileFacade;

    public FilesController(IFileFacade fileFacade) {
        this.fileFacade = fileFacade;
    }


    [HttpGet("{guid}")]
    public FileStreamResult Get(Guid guid)
    {
        var file = fileFacade.GetFileByGuid(guid);
        var stream = new MemoryStream(file.Data);
        return new FileStreamResult(stream, file.FileType) {
          FileDownloadName = file.Name
        };
    }
}
