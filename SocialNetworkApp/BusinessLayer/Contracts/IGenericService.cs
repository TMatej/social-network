using DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
    public interface IGenericService<TEntity> 
        where TEntity : class
    {
        IEnumerable<TDTO> GetAll<TDTO>() where TDTO : class;

        TDTO GetByID<TDTO>(object id) where TDTO : class;

        void Insert<TDTO>(TDTO entity) where TDTO : class;
        void Delete(object id);

        void Delete<TDTO>(TDTO entityToDelete) where TDTO : class;

        void Update<TDTO>(TDTO entityToUpdate) where TDTO : class;
    }
}
