using AutoMapper;
using BusinessLayer.DTOs.Gallery;
using BusinessLayer.DTOs.Gallery.Filters;
using BusinessLayer.DTOs.Gallery.Results;
using DataAccessLayer.Entity;
using Infrastructure.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.QueryObjects
{
    public class GalleryQueryObject : GenericQueryObject<Gallery>
    {
        public GalleryQueryObject(IMapper mapper, IQuery<Gallery> galleryQuery) : base(mapper, galleryQuery)
        { }

        protected override IQuery<Gallery> ApplyWhere(GenericFilterDTO filter)
        {
            var query = _entityQuery
                .Where(filter.FilterWhereExpression, filter.WhereColumnName);
            return query;
        }
    }
}
