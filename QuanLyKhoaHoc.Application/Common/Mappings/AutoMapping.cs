using AutoMapper;
using QuanLyKhoaHoc.Domain.Entities;

namespace QuanLyKhoaHoc.Application.Common.Mappings
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Subject, SubjectMapping>().ReverseMap();
            CreateMap<Subject, SubjectCreate>().ReverseMap();
            CreateMap<Subject, SubjectUpdate>().ReverseMap();

            CreateMap<Course, CourseMapping>().ReverseMap();
            CreateMap<Course, CourseCreate>().ReverseMap();
            CreateMap<Course, CourseUpdate>().ReverseMap();

            CreateMap<Province, AdressMapping>().ReverseMap();
            CreateMap<Ward, AdressMapping>().ReverseMap();
            CreateMap<District, AdressMapping>().ReverseMap();

            CreateMap<Province, createAdress>().ReverseMap();
            CreateMap<Ward, createAdress>().ReverseMap();
            CreateMap<District, createAdress>().ReverseMap();

            CreateMap<Province, UpdateAdress>().ReverseMap();
            CreateMap<Ward, UpdateAdress>().ReverseMap();
            CreateMap<District, UpdateAdress>().ReverseMap();

        }
    }
}
