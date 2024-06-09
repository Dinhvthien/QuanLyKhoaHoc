using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using QuanLyKhoaHoc.Application.Common;
using QuanLyKhoaHoc.Application.Common.Interfaces;
using QuanLyKhoaHoc.Application.Common.Mappings;
using QuanLyKhoaHoc.Application.Common.Models;
using QuanLyKhoaHoc.Domain.Entities;

namespace QuanLyKhoaHoc.Application.Services
{
    public class CourseService : ApplicationServiceBase<CourseMapping, CourseQuery, CourseCreate, CourseUpdate>
    {
        public CourseService(IApplicationDbContext context, IMapper mapper, IUser user) : base(context, mapper, user)
        {
        }

        public override async Task<Result> Create(CourseCreate entity, CancellationToken cancellation)
        {
            try
            {
                if (_user.Id == null) return new Result(Domain.ResultStatus.Forbidden, "Bạn Chưa Đăng Nhập");
                var getCertificate = await _context.Certificates.SingleOrDefaultAsync(c => c.Id == _user.Certificateid, cancellation);
                if (getCertificate == null)
                {
                    return new Result(Domain.ResultStatus.Forbidden, "Bạn Chưa Có chứng chỉ giảng viên");
                }
                var getCertificateType = await _context.CertificateTypes.SingleOrDefaultAsync(c => c.Id == getCertificate.CertificateTypeId, cancellation);

                if (getCertificateType == null)
                {
                    return new Result(Domain.ResultStatus.Forbidden, "Bạn Chưa Có chứng chỉ giảng viên");
                }

                if (!getCertificateType.Name.Trim().Equals("giang vien", StringComparison.CurrentCultureIgnoreCase))
                {
                    return new Result(Domain.ResultStatus.Forbidden, "Bạn Chưa Có chứng chỉ giảng viên");
                }
                var course = _mapper.Map<Course>(entity);

                course.CreatorId = int.Parse(_user.Id);

                await _context.Courses.AddAsync(course, cancellation);

                var result = await _context.SaveChangesAsync(cancellation);

                if (result != 1)
                {
                    return Result.Failure("Lỗi Gì Đó");
                }
                var Coursesb = new List<CourseSubject>();
                foreach (var item in entity.SubjectId)
                {
                    var CourseSubject = new CourseSubject();
                    CourseSubject.SubjectId = item;
                    CourseSubject.CourseId = course.Id;
                    Coursesb.Add(CourseSubject);
                }

                await _context.CourseSubjects.AddRangeAsync(Coursesb, cancellation);

                var resultCourseSubjects = await _context.SaveChangesAsync(cancellation);
                if (resultCourseSubjects != 1)
                {
                    return Result.Failure("Lỗi Gì Đó");
                }

                var ListCourseUserCreate = await _context.Courses.Where(c => c.CreatorId == course.CreatorId).ToListAsync();
                var courseIdList = ListCourseUserCreate.Select(c => c.Id).ToList(); // Lấy danh sách các CourseId

                var RegisterStudy = await _context.RegisterStudys
                    .Where(rs => courseIdList.Contains(rs.CourseId))
                    .ToListAsync();
                if (RegisterStudy == null|| RegisterStudy.Count ==0)
                {
                    return new Result(Domain.ResultStatus.Succeess, "Thêm thành công nhưng bạn chưa có sinh viên nào để nhận thông báo ");
                }
                else
                {
                    foreach (var userId in RegisterStudy)
                    {
                        var noti = new Notification()
                        {
                            Content = entity.Name,
                            UserId = course.CreatorId,
                            Image = entity.ImageCourse,
                            Link = "Your notification link here",
                            IsSeen = false,
                            CreateTime = DateTime.Now,
                        };

                        await _context.Notifications.AddAsync(noti);
                    }
                    await _context.SaveChangesAsync(cancellation);
                    return new Result(Domain.ResultStatus.Succeess, "Thêm thành công Những sinh viên đăng ký khóa học khác của bạn sẽ nhận được thông báo về khóa học này");

                }
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public override async Task<Result> Delete(int id, CancellationToken cancellation)
        {
            try
            {
                if (_user.Id == null) return new Result(Domain.ResultStatus.Forbidden, "Bạn Chưa Đăng Nhập");

                var course = await _context.Courses.FindAsync(new object[] { id }, cancellation);

                if (course == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                if (course.CreatorId != int.Parse(_user.Id))
                {
                    return new Result(Domain.ResultStatus.Forbidden, "Bạn Không Thể Xóa Khóa Học Này");
                }
                var findAllCoursesSb = _context.CourseSubjects.Where(x => x.CourseId == course.Id).ToList();
                _context.CourseSubjects.RemoveRange(findAllCoursesSb);
                var resultCourseSubjects = await _context.SaveChangesAsync(cancellation);
                if (resultCourseSubjects != 1)
                {
                    return Result.Failure("Lỗi Gì Đó");
                }
                _context.Courses.Remove(course);

                var result = await _context.SaveChangesAsync(cancellation);

                if (result != 1)
                {
                    return Result.Failure("Lỗi Gì Đó");
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public override async Task<PagingModel<CourseMapping>> Get(CourseQuery query, CancellationToken cancellation)
        {
            var courses = _context.Courses.AsNoTracking().Include(c => c.CourseSubjects)
            .ThenInclude(cs => cs.Subject); ;

            var totalCount = await courses.ApplyQuery(query, applyPagination: false).CountAsync(cancellation);

            var data = await courses
                .ApplyQuery(query)
                .ProjectTo<CourseMapping>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation);

            return new PagingModel<CourseMapping>(data, totalCount, query.Page ?? 1, query.PageSize ?? 10);
        }

        public override async Task<CourseMapping?> Get(int id, CancellationToken cancellation)
        {
            var course = await _context.Courses.FindAsync(new object[] { id }, cancellation);

            if (course == null)
            {
                return null;
            }

            return _mapper.Map<CourseMapping>(course);
        }

        public override async Task<Result> Update(int id, CourseUpdate entity, CancellationToken cancellation)
        {
            try
            {
                if (_user.Id == null) return new Result(Domain.ResultStatus.Forbidden, "Bạn Chưa Đăng Nhập");

                if (entity.Id != id)
                {
                    return Result.Failure("ID Phải Giống Nhau");
                }

                var course = await _context.Courses.FindAsync(new object[] { id }, cancellation);

                if (course == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                if (course.CreatorId != int.Parse(_user.Id))
                {
                    return new Result(Domain.ResultStatus.Forbidden, "Bạn Không Thể Sửa Khóa Học Này");
                }
                var existingCourseSubjects = _context.CourseSubjects.Where(c => c.CourseId == course.Id).ToList();

                var existingSubjectIds = existingCourseSubjects.Select(cs => cs.SubjectId).ToList();
                var newSubjectIds = entity.Subject;

                // Các Subject cần thêm
                var subjectsToAdd = newSubjectIds.Except(existingSubjectIds).ToList();

                // Các Subject cần xóa      
                var subjectsToRemove = existingSubjectIds.Except(newSubjectIds).ToList();

                // Thêm các Subject mới
                foreach (var subjectId in subjectsToAdd)
                {
                    var courseSubjectexit = existingCourseSubjects.FirstOrDefault(cs => cs.SubjectId == subjectId);
                    if (courseSubjectexit != null)
                    {
                        return new Result(Domain.ResultStatus.NotFound, $"Subject {subjectId} không tồn tại");
                    }
                    var courseSubject = new CourseSubject
                    {
                        CourseId = course.Id,
                        SubjectId = subjectId
                    };
                    _context.CourseSubjects.Add(courseSubject);
                }

                // Xóa các Subject không còn trong entity
                foreach (var subjectId in subjectsToRemove)
                {
                    var courseSubject = existingCourseSubjects.FirstOrDefault(cs => cs.SubjectId == subjectId);
                    if (courseSubject != null)
                    {
                        _context.CourseSubjects.Remove(courseSubject);
                    }
                }

                //_mapper.Map(entity, course);

                _context.Courses.Update(course);

                var result = await _context.SaveChangesAsync(cancellation);

                return new Result(Domain.ResultStatus.Succeess, "Sửa thành công");

            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public override Task<Result> Updatemore(CourseUpdate entity, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}
