using BusinessLayer.DTOs.Comment;
using BusinessLayer.DTOs.Search;
using BusinessLayer.Facades;
using BusinessLayer.Facades.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        readonly ISearchFacade searchFacade;

        public SearchController(ISearchFacade searchFacade)
        {
            this.searchFacade = searchFacade;
        }

        ///search/users?username={username}&page={page}&size={size}

        [HttpGet("search/users")]
        public IActionResult SearchUsers(string name, int page, int size)
        {
            var users = searchFacade.FindUser(name, size, page);
            var paginated = new Paginated<SearchResultDTO>()
            {
                Items = users,
                Page = page,
                Size = size,
            };
            return Ok(paginated);
        }


        ///search/events?name={name}&page={page}&size={size}

        [HttpGet("search/events")]
        public IActionResult SearchEvents(string name, int page, int size)
        {
            var events = searchFacade.FindEvent(name,size,page);
            var paginated = new Paginated<SearchResultDTO>()
            {
                Items = events,
                Page = page,
                Size = size,
            };
            return Ok(paginated);
        }

        ///search/groups?name={name}&page={page}&size={size}

        [HttpGet]
        public IActionResult SearchGroups(string name, int page, int size)
        {
            var groups = searchFacade.FindGroup(name,size,page);
            var paginated = new Paginated<SearchResultDTO>()
            {
                Items = groups,
                Page = page,
                Size = size,
            };
            return Ok(paginated);
        }

        ///search?value={value}&page={page}&size={size}

        [HttpGet]
        public IActionResult SearchAll(string name, int page, int size)
        {
            var all = searchFacade.FindAll(name, size, page);
            var paginated = new Paginated<SearchResultDTO>()
            {
                Items = all,
                Page = page,
                Size = size,
            };
            return Ok(paginated);
        }

    }
}
