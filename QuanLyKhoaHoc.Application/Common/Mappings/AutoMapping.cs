using AutoMapper;
using QuanLyKhoaHoc.Domain.Entities;

namespace QuanLyKhoaHoc.Application.Common.Mappings
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Course, CourseMapping>()
            .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.CourseSubjects.Select(cs => cs.Subject)));
            CreateMap<Subject, SubjectMapping>().ReverseMap();
            CreateMap<Subject, SubjectCreate>().ReverseMap();
            CreateMap<Subject, SubjectUpdate>().ReverseMap();

         
            CreateMap<Course, CourseCreate>().ReverseMap();
            CreateMap<Course, CourseUpdate>().ReverseMap();

            CreateMap<Province, AdressMapping>().ReverseMap();
            CreateMap<Ward, AdressMapping>().ReverseMap();
            CreateMap<District, AdressMapping>().ReverseMap();

            CreateMap<Province,CreateAdress>().ReverseMap();
            CreateMap<Ward, CreateAdress>().ReverseMap();
            CreateMap<District, CreateAdress>().ReverseMap();

            CreateMap<Province, UpdateAdress>().ReverseMap();
            CreateMap<Ward, UpdateAdress>().ReverseMap();
            CreateMap<District, UpdateAdress>().ReverseMap();

        }
    }
}
