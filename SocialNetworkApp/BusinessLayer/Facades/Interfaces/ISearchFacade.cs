using BusinessLayer.DTOs.Event;
using BusinessLayer.DTOs.Group;
using BusinessLayer.DTOs.Search;
using BusinessLayer.DTOs.User;
using DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
