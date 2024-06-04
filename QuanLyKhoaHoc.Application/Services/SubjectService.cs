using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using QuanLyKhoaHoc.Application.Common.Interfaces;
using QuanLyKhoaHoc.Application.Common.Mappings;
using QuanLyKhoaHoc.Application.Common.Models;
using QuanLyKhoaHoc.Domain.Entities;
using System.Linq;

namespace QuanLyKhoaHoc.Application.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SubjectService(IApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> CreateSubject(SubjectCreate entity, CancellationToken cancellationToken)
        {
            try
            {
                await _context.Subjects.AddAsync(_mapper.Map<Subject>(entity));

                var result = await _context.SaveChangesAsync(cancellationToken);

                if (result != 1) {
                    return Result.Failure("Lỗi Gì Đó");
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public async Task<Result> DeleteSubject(int id, CancellationToken cancellationToken)
        {
            try
            {
                var subject = _context.Subjects.FirstOrDefault(c => c.Id == id);

                if (subject == null) return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");

                _context.Subjects.Remove(subject);

                var result = await _context.SaveChangesAsync(cancellationToken);

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

        public async Task<SubjectMapping?> GetSubject(int id, CancellationToken cancellationToken)
        {
            var subject = await _context.Subjects.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

            if (subject == null) return null;

            return _mapper.Map<SubjectMapping>(subject);
        }

        public async Task<PagingModel<SubjectMapping>> GetSubjects(SubjectQuery query, CancellationToken cancellationToken)
        {
            var subjects = _context.Subjects.AsNoTracking();

            var totalCount = await subjects.CountAsync(cancellationToken);

            subjects = subjects.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize);

            var data = await subjects.ProjectTo<SubjectMapping>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

            return new PagingModel<SubjectMapping>(data, totalCount, query.Page, query.PageSize);
        }

        public async Task<Result> UpdateSubject(int id, SubjectUpdate entity, CancellationToken cancellationToken)
        {
            try
            {
                if (entity.Id != id) return Result.Failure("ID Phải Giống Nhau");

                var subject = await _context.Subjects.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

                if (subject == null) return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");

                _context.Subjects.Update(_mapper.Map<Subject>(entity));

                var result = await _context.SaveChangesAsync(cancellationToken);

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
