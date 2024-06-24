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
using QuanLyKhoaHoc.Domain.Entities;
using AutoMapper.QueryableExtensions;

namespace QuanLyKhoaHoc.Application.Services
{
    public class CertificateService : ApplicationServiceBase<CertificateMapping, CertificateQuery, CreateCertificate, UpdateCertificate>
    {
        public CertificateService(IApplicationDbContext context, IMapper mapper, IUser user) : base(context, mapper, user)
        {
        }

        public override async Task<Result> Create(CreateCertificate entity, CancellationToken cancellation)
        {
            try
            {
                //if (_user.Id == null) return new Result(Domain.ResultStatus.Forbidden, "Bạn Chưa Đăng Nhập");
                //var getCertificate = await _context.Certificates.SingleOrDefaultAsync(c => c.Id == _user.Certificateid, cancellation);
                //if (getCertificate == null)
                //{
                //    return new Result(Domain.ResultStatus.Forbidden, "Bạn Chưa Có chứng chỉ giảng viên");
                //}
                //var getCertificateType = await _context.CertificateTypes.SingleOrDefaultAsync(c => c.Id == getCertificate.CertificateTypeId, cancellation);

                //if (getCertificateType == null)
                //{
                //    return new Result(Domain.ResultStatus.Forbidden, "Bạn Chưa Có chứng chỉ giảng viên");
                //}
                //if (getCertificateType.Id != 1)
                //{
                //    return new Result(Domain.ResultStatus.Forbidden, "Bạn Chưa Có chứng chỉ giảng viên");
                //}

                var certificatelist = new List<Certificate>();
                foreach (var item in entity.CertificateTypeId)
                {
                    var findCertificateType = await _context.CertificateTypes.SingleOrDefaultAsync(c => c.Id == item, cancellation);
                    if (findCertificateType == null)
                    {
                        return new Result(Domain.ResultStatus.Failure, "Loại chứng chỉ này chưa có");
                    }
                    certificatelist.Add(new Certificate
                    {
                        Name = entity.Name.Trim(),
                        Description = entity.Description.Trim(),
                        Image = entity.Image.Trim(),
                        CertificateTypeId = item,
                        CertificateType = findCertificateType,
                    });
                }

                await _context.Certificates.AddRangeAsync(certificatelist);

                var result = await _context.SaveChangesAsync(cancellation);

                return new Result(Domain.ResultStatus.Succeess, "Thêm chứng chỉ thành công");
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
                var certificateType = await _context.Certificates.FindAsync(new object[] { id }, cancellation);
                if (certificateType == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }
                _context.Certificates.Remove(certificateType);
                var result = await _context.SaveChangesAsync(cancellation);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public override async Task<PagingModel<CertificateMapping>> Get(CertificateQuery query, CancellationToken cancellation)
        {
            var courses = _context.Certificates.AsNoTracking().Include(c => c.CertificateType);


            var totalCount = await courses.ApplyQuery(query, applyPagination: false).CountAsync(cancellation);

            var data = await courses
                .ApplyQuery(query)
                .ProjectTo<CertificateMapping>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation);

            return new PagingModel<CertificateMapping>(data, totalCount, query.Page ?? 1, query.PageSize ?? 10);
        }

        public override Task<CertificateMapping?> Get(int id, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        public override async Task<Result> Update(int id, UpdateCertificate entity, CancellationToken cancellation)
        {
            try
            {
                if (entity.Id != id)
                {
                    return Result.Failure("ID Phải Giống Nhau");
                }

                var certificate = await _context.Certificates.FindAsync(new object[] { id }, cancellation);

                if (certificate == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                certificate.Name = entity.Name.Trim();
                certificate.Description = entity.Description.Trim();
                certificate.Image = entity.Image.Trim();
                certificate.CertificateTypeId = entity.CertificateTypeId;

                _context.Certificates.Update(certificate);
                await _context.SaveChangesAsync(cancellation);
                return new Result(Domain.ResultStatus.Succeess, "Sửa thành công");

            }
            catch (Exception)
            {

                return new Result(Domain.ResultStatus.Failure, "Có lỗi xẩy ra");

            }
        }

        public override Task<Result> Updatemore(UpdateCertificate entity, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}
