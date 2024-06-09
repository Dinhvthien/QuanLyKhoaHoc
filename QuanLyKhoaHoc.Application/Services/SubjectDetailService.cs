using QuanLyKhoaHoc.Application.Common.Mappings;
using QuanLyKhoaHoc.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using QuanLyKhoaHoc.Application.Common.Interfaces;
using QuanLyKhoaHoc.Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using QuanLyKhoaHoc.Domain.Entities;

namespace QuanLyKhoaHoc.Application.Services
{
    public class SubjectDetailService : ApplicationServiceBase<SubjectDetailMapping, SubjectDetailQuery, SubjectDetailCreate, SubjectDetailUpdate>
    {
        public SubjectDetailService(IApplicationDbContext context, IMapper mapper, IUser user) : base(context, mapper, user)
        {
        }

        public override async Task<Result> Create(SubjectDetailCreate entity, CancellationToken cancellation)
        {
            try
            {
                if (entity.LinkVideo.Trim() == "" || entity.Name.Trim() == "" || entity.SubjectId == 0)
                {
                    return new Result(Domain.ResultStatus.Forbidden, "Bạn cần nhập đủ thông tin");
                }
                if (!_context.Subjects.Any(c=>c.Id == entity.SubjectId))
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy subject");
                }

                var subjectdetail = _mapper.Map<SubjectDetail>(entity);
                await _context.SubjectDetails.AddAsync(subjectdetail, cancellation);

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
                var subjectdetail = await _context.SubjectDetails.FindAsync(new object[] { id }, cancellation);

                if (subjectdetail == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _context.SubjectDetails.Remove(subjectdetail);

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

        public override async Task<PagingModel<SubjectDetailMapping>> Get(SubjectDetailQuery query, CancellationToken cancellation)
        {
            var subjectdetails = _context.SubjectDetails.AsNoTracking();

            var totalCount = await subjectdetails.ApplyQuery(query, applyPagination: false).CountAsync(cancellationToken: cancellation);

            var data = await subjectdetails
                .ApplyQuery(query)
                .ProjectTo<SubjectDetailMapping>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation);
            return new PagingModel<SubjectDetailMapping>(data, totalCount, query.Page ?? 1, query.PageSize ?? 10);
        }

        public override async Task<SubjectDetailMapping?> Get(int id, CancellationToken cancellation)
        {
            var subjectdetails = await _context.SubjectDetails.FindAsync(new object[] { id }, cancellation);

            if (subjectdetails == null)
            {
                return null;
            }

            return _mapper.Map<SubjectDetailMapping>(subjectdetails);
        }

        public override async Task<Result> Update(int id, SubjectDetailUpdate entity, CancellationToken cancellation)
        {
            try
            {
                if (entity.Id != id)
                {
                    return Result.Failure("ID Phải Giống Nhau");
                }

                var subjectdetail = await _context.SubjectDetails.FindAsync(new object[] { id }, cancellation);

                if (subjectdetail == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _mapper.Map(entity, subjectdetail);

                _context.SubjectDetails.Update(subjectdetail);

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

        public override Task<Result> Updatemore(SubjectDetailUpdate entity, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}
