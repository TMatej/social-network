using BusinessLayer.DTOs.Photo;
using BusinessLayer.Facades.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;

[ApiController]
[Route("[controller]")]
public class GalleriesController : ControllerBase
{
    IGalleryFacade galleryFacade;

    public GalleriesController(IGalleryFacade galleryFacade)
    {
       this.galleryFacade = galleryFacade;
    }

    // Add check if its authenticated users gallery
    [Authorize]
    [HttpPost("{galleryId}/photos")]
    public IActionResult PostPhoto(int galleryId, [FromForm] PhotoCreateDTO photoCreateDTO)
    {
        galleryFacade.PostPhoto(galleryId, photoCreateDTO);
        return Ok();
    }

    [Authorize]
    [HttpDelete("{galleryId}/photos/{photoId}")]
    public IActionResult DeletePhoto(int galleryId, int photoId)
    {
        galleryFacade.DeletePhoto(galleryId, photoId);
        return Ok();
    }
}
