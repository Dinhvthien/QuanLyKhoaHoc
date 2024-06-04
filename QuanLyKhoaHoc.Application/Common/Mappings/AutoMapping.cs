using AutoMapper;
using QuanLyKhoaHoc.Domain.Entities;

namespace QuanLyKhoaHoc.Application.Common.Mappings
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Subject, SubjectMapping>();
            CreateMap<Subject, SubjectCreate>().ReverseMap();
            CreateMap<Subject, SubjectUpdate>().ReverseMap();

            CreateMap<Course, CourseMapping>();
            CreateMap<Course, CourseCreate>();
            CreateMap<Course, CourseUpdate>();
        }
    }
}
