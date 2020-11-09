using System;
using AutoMapper;
using CourseLibrary.API.Models;

namespace CourseLibrary.API.Profiles
{
    public class CoursesProfile : Profile
    {
        public CoursesProfile()
        {
            CreateMap<Entities.Course, Models.Course>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src =>
                $"{src.Author.FirstName} {src.Author.LastName}"));

            CreateMap<Models.CourseForCreation, Entities.Course>();
        }
    }
}
