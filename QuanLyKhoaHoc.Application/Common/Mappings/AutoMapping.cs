using AutoMapper;
using QuanLyKhoaHoc.Application.Payload.Requests;
using QuanLyKhoaHoc.Application.Payload.Responses;
using QuanLyKhoaHoc.Domain.Entities;

namespace QuanLyKhoaHoc.Application.Common.Mappings
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Course, CourseMapping>()
            .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.CourseSubjects.Select(cs => cs.Subject)));
            CreateMap<Course, CourseCreate>().ReverseMap();
            CreateMap<Course, CourseUpdate>().ReverseMap();

            CreateMap<Subject, SubjectMapping>().ReverseMap();
            CreateMap<Subject, SubjectCreate>().ReverseMap();
            CreateMap<Subject, SubjectUpdate>().ReverseMap();

            CreateMap<SubjectDetail, SubjectDetailMapping>().ReverseMap();
            CreateMap<SubjectDetail, SubjectDetailCreate>().ReverseMap();
            CreateMap<SubjectDetail, SubjectDetailUpdate>().ReverseMap();


            CreateMap<CertificateType, CertificateTypeMapping>().ReverseMap();
            CreateMap<CertificateType, CreateCertificateType>().ReverseMap();
            CreateMap<CertificateType, UpdateCertificateType>().ReverseMap();

            //CreateMap<Certificate, CreateCertificate>().ForMember(dest => dest.CertificateTypeId, opt => opt.Ignore());
            CreateMap<Certificate, CertificateMapping>()
             .ForMember(dest => dest.CertificateType, opt => opt.MapFrom(src => new List<CertificateType> { src.CertificateType }));

            CreateMap<CertificateMapping, Certificate>()
                .ForMember(dest => dest.CertificateType, opt => opt.MapFrom(src => src.CertificateType.Any()));
            //CreateMap<Certificate, UpdateCertificate>().ReverseMap();

            //CreateMap<Province, AdressMapping>().ReverseMap();
            //CreateMap<Ward, AdressMapping>().ReverseMap();
            //CreateMap<District, AdressMapping>().ReverseMap();

            //CreateMap<Province, CreateAdress>().ReverseMap();
            //CreateMap<Ward, CreateAdress>().ReverseMap();
            //CreateMap<District, CreateAdress>().ReverseMap();

            //CreateMap<Province, UpdateAdress>().ReverseMap();
            //CreateMap<Ward, UpdateAdress>().ReverseMap();
            //CreateMap<District, UpdateAdress>().ReverseMap();

            CreateMap<UserRegisterRequest, User>();
            CreateMap<User, UserRegisterResponse>();
            CreateMap<UserUpdateRequest, User>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember) => srcMember != null)
                );
            CreateMap<User, UserGetResponse>();
        }
    }
}
