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
using QuanLyKhoaHoc.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace QuanLyKhoaHoc.Application.Services
{
    public class CertificateTypeService : ApplicationServiceBase<CertificateTypeMapping, CertificateTypeQuery, CreateCertificateType, UpdateCertificateType>
    {
        public CertificateTypeService(IApplicationDbContext context, IMapper mapper, IUser user) : base(context, mapper, user)
        {

        }

        public override async Task<Result> Create(CreateCertificateType entity, CancellationToken cancellation)
        {
            try
            {
                if (entity.Name.Trim() == "")
                {
                    return new Result(Domain.ResultStatus.Forbidden, "Bạn cần nhập đủ thông tin");
                }
                var ceterificateType= _mapper.Map<CertificateType>(entity);
                await _context.CertificateTypes.AddAsync(ceterificateType, cancellation);

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
                var certificateType = await _context.CertificateTypes.FindAsync(new object[] { id }, cancellation);

                if (certificateType == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _context.CertificateTypes.Remove(certificateType);

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

        public override async Task<PagingModel<CertificateTypeMapping>> Get(CertificateTypeQuery query, CancellationToken cancellation)
        {
            var ceterificateTypes = _context.CertificateTypes.AsNoTracking();

            var totalCount = await ceterificateTypes.ApplyQuery(query, applyPagination: false).CountAsync(cancellationToken: cancellation);

            var data = await ceterificateTypes
                .ApplyQuery(query)
                .ProjectTo<CertificateTypeMapping>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation);
            return new PagingModel<CertificateTypeMapping>(data, totalCount, query.Page ?? 1, query.PageSize ?? 10);
        }

        public override async Task<CertificateTypeMapping?> Get(int id, CancellationToken cancellation)
        {
            var subject = await _context.CertificateTypes.FindAsync(new object[] { id }, cancellation);

            if (subject == null)
            {
                return null;
            }

            return _mapper.Map<CertificateTypeMapping>(subject);
        }

        public override async Task<Result> Update(int id, UpdateCertificateType entity, CancellationToken cancellation)
        {
            try
            {
                if (entity.Id != id)
                {
                    return Result.Failure("ID Phải Giống Nhau");
                }

                var certificateType = await _context.CertificateTypes.FindAsync(new object[] { id }, cancellation);

                if (certificateType == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _mapper.Map(entity, certificateType);

                _context.CertificateTypes.Update(certificateType);

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

        public override Task<Result> Updatemore(UpdateCertificateType entity, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}
