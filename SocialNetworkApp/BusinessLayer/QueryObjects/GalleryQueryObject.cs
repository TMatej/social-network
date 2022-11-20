using AutoMapper;
using BusinessLayer.DTOs.Query;
using BusinessLayer.DTOs.Query.Filters;
using BusinessLayer.DTOs.Query.Results;
using DataAccessLayer.Entity;
using Infrastructure.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.QueryObjects
{
    public class GalleryQueryObject
    {
        private IMapper _mapper;
        private IQuery<Gallery> _galleryQuery;

        public GalleryQueryObject(IMapper mapper, IQuery<Gallery> galleryQuery)
        {
            _mapper = mapper;
            _galleryQuery = galleryQuery;
        }

        /* MIGHT BE USED FOR CUSTOM IMPLEMENTATION OF SPECIFIC FUNCTIONALITY */
    }
}
