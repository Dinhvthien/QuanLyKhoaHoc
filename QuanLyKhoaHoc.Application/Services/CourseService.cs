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
                if(_user.Id == null) return new Result(Domain.ResultStatus.Forbidden, "Bạn Chưa Đăng Nhập");

                var course = _mapper.Map<Course>(entity);

                course.CreatorId = int.Parse(_user.Id);

                await _context.Courses.AddAsync(course, cancellation);

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
            var courses = _context.Courses.AsNoTracking();

            var totalCount = await courses.ApplyQuery(query, applyPagination: false).CountAsync();

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

                _mapper.Map(entity, course);

                _context.Courses.Update(course);

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
    }
}
