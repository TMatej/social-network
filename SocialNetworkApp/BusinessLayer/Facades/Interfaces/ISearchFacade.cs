using BusinessLayer.DTOs.Search;

namespace BusinessLayer.Facades.Interfaces
{
    public interface ISearchFacade
    {
        public IEnumerable<SearchResultDTO> FindEvent(string name, int pageSize, int page);
        public IEnumerable<SearchResultDTO> FindGroup(string name, int pageSize, int page);
        public IEnumerable<SearchResultDTO> FindUser(string name, int pageSize, int page);
        public IEnumerable<SearchResultDTO> FindAll(string name, int pageSize, int page);
    }
}
