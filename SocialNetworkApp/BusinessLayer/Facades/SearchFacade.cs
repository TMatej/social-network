using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs.Event;
using BusinessLayer.DTOs.Group;
using BusinessLayer.DTOs.Search;
using BusinessLayer.DTOs.User;
using BusinessLayer.Facades.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.Facades
{
    public class SearchFacade : ISearchFacade
    {
        readonly IUserService userService;
        readonly IEventService eventService;
        readonly IGroupService groupService;
        readonly IMapper mapper;

        public SearchFacade(IUserService userService, IEventService eventService, IGroupService groupService, IMapper mapper)
        {
            this.userService = userService;
            this.eventService = eventService;
            this.groupService = groupService;
            this.mapper = mapper;
        }

        public IEnumerable<SearchResultDTO> FindAll(string name, int pageSize, int page)
        {
            var events = eventService.Find(name);
            var eventsDTO = events.Select(e => mapper.Map<EventRepresentDTO>(e)) as List<SearchableDTO>;

            var users = userService.FindByName(name);
            var usersDTO = users.Select(u => mapper.Map<UserDTO>(u)) as List<SearchableDTO>;

            var groups = groupService.FindByName(name);
            var groupsDTO = groups.Select(g => mapper.Map<GroupRepresentDTO>(g)) as List<SearchableDTO>;

            var joined = eventsDTO.Concat(usersDTO).Concat(groupsDTO);

            var start = pageSize * page;

            var paginated = joined.Skip(start).Take(pageSize);

            var dtos = paginated.Select(p => new SearchResultDTO()
            {
                searchable = p,
                type = p.GetType(),
            });

            return dtos;
        }

        public IEnumerable<SearchResultDTO> FindEvent(string name, int pageSize, int page)
        {
            var events = eventService.Find(name, pageSize, page);
            var eventsDTO = events.Select(e => mapper.Map<EventRepresentDTO>(e)) as List<SearchableDTO>;
            var dtos = eventsDTO.Select(p => new SearchResultDTO()
            {
                searchable = p,
                type = p.GetType(),
            });
            return dtos;
        }

        public IEnumerable<SearchResultDTO> FindGroup(string name, int pageSize, int page)
        {
            var groups = groupService.FindByName(name, pageSize, page);
            var eventsDTO = groups.Select(e => mapper.Map<GroupRepresentDTO>(e)) as List<SearchableDTO>;
            var dtos = eventsDTO.Select(p => new SearchResultDTO()
            {
                searchable = p,
                type = p.GetType(),
            });
            return dtos;
        }

        public IEnumerable<SearchResultDTO> FindUser(string name, int pageSize, int page)
        {
            var users = userService.FindByName(name, pageSize, page);
            var usersDTO = users.Select(u => mapper.Map<UserDTO>(u)) as List<SearchableDTO>;
            var dtos = usersDTO.Select(p => new SearchResultDTO()
            {
                searchable = p,
                type = p.GetType(),
            });
            return dtos;
        }

    }
}
