using AutoMapper;
using Course.Services.Catalog.Dtos;
using Course.Services.Catalog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Catalog.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<Courses, CourseDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();


            CreateMap<Courses, CourseCreateDto>().ReverseMap();
            CreateMap<Courses, CourseUpdateDto>().ReverseMap();

        }
    }
}
