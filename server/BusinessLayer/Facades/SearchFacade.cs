using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs.Search;
using BusinessLayer.Facades.Interfaces;

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
            var eventsDTO = events.Select(e => mapper.Map<SearchResultDTO>(e));

            var users = userService.FindByName(name);
            var usersDTO = users.Select(u => mapper.Map<SearchResultDTO>(u));

            var groups = groupService.FindByName(name);
            var groupsDTO = groups.Select(g => mapper.Map<SearchResultDTO>(g));

            var joined = eventsDTO.Concat(usersDTO).Concat(groupsDTO);

            var start = pageSize * (page - 1);

            return joined.Skip(start).Take(pageSize);
        }

        public IEnumerable<SearchResultDTO> FindEvent(string name, int pageSize, int page)
        {
            var events = eventService.Find(name, pageSize, page);
            return events.Select(e => mapper.Map<SearchResultDTO>(e));
        }

        public IEnumerable<SearchResultDTO> FindGroup(string name, int pageSize, int page)
        {
            var groups = groupService.FindByName(name, pageSize, page);
            return groups.Select(e => mapper.Map<SearchResultDTO>(e));
        }

        public IEnumerable<SearchResultDTO> FindUser(string name, int pageSize, int page)
        {
            var users = userService.FindByName(name, pageSize, page);
            return users.Select(u => mapper.Map<SearchResultDTO>(u));
        }
    }
}
