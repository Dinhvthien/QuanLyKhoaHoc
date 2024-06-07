﻿using QuanLyKhoaHoc.Application.Common.Mappings;
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
    public class AdressService : ApplicationServiceBase<AdressMapping, AdressQuery, createAdress, UpdateAdress>
    {
        public AdressService(IApplicationDbContext context, IMapper mapper, IUser user) : base(context, mapper, user)
        {

        }

        public override async Task<Result> Create(createAdress entity, CancellationToken cancellation)
        {
            try
            {
                if (entity.NameDisTrict.Trim() == "" || entity.NameWard.Trim() == "" || entity.NameProvince.Trim() == "")
                {
                    return new Result(Domain.ResultStatus.Failure, "Bạn cần phải nhập đủ thông tin");
                }
                //// Kiểm tra xem người dùng đã đăng nhập hay chưa
                //if (_user.Id == null)
                //{
                //    return new Result(Domain.ResultStatus.Forbidden, "Bạn Chưa Đăng Nhập");
                //}

                // Tìm kiếm tỉnh/thành phố
                var findProvince = await _context.Provinces.SingleOrDefaultAsync(c => c.Name.Trim().ToLower() == entity.NameProvince.Trim().ToLower(), cancellation);

                // Tìm kiếm quận/huyện
                var findDistrict = await _context.Districts.SingleOrDefaultAsync(c => c.Name.Trim().ToLower() == entity.NameDisTrict.Trim().ToLower(), cancellation);

                // Tìm kiếm phường/xã
                var findWard = await _context.Wards.SingleOrDefaultAsync(c => c.Name.Trim().ToLower() == entity.NameWard.Trim().ToLower(), cancellation);

                // Nếu tỉnh/thành phố tồn tại
                if (findProvince != null)
                {
                    // Nếu quận/huyện không tồn tại hoặc không thuộc tỉnh/thành phố đó
                    if (findDistrict == null || findDistrict.ProvinceId != findProvince.Id)
                    {
                        District district = new District
                        {
                            Name = entity.NameDisTrict.Trim(),
                            ProvinceId = findProvince.Id
                        };
                        await _context.Districts.AddAsync(district, cancellation);
                        await _context.SaveChangesAsync(cancellation);

                        Ward ward = new Ward
                        {
                            Name = entity.NameWard.Trim(),
                            DistrictId = district.Id
                        };
                        await _context.Wards.AddAsync(ward, cancellation);
                        await _context.SaveChangesAsync(cancellation);

                        return Result.Success();
                    }
                    else
                    {
                        // Nếu quận/huyện tồn tại và đúng tỉnh/thành phố
                        if (findWard == null)
                        {
                            Ward ward = new Ward
                            {
                                Name = entity.NameWard.Trim(),
                                DistrictId = findDistrict.Id
                            };
                            await _context.Wards.AddAsync(ward, cancellation);
                            await _context.SaveChangesAsync(cancellation);

                            return Result.Success();
                        }
                        else
                        {
                            return new Result(Domain.ResultStatus.Failure, "Phường/Xã đã tồn tại trong hệ thống");
                        }
                    }
                }
                else
                {
                    // Nếu tỉnh/thành phố không tồn tại
                    Province province = new Province
                    {
                        Name = entity.NameProvince.Trim()
                    };
                    await _context.Provinces.AddAsync(province, cancellation);
                    await _context.SaveChangesAsync(cancellation);

                    District district = new District
                    {
                        Name = entity.NameDisTrict.Trim(),
                        ProvinceId = province.Id
                    };
                    await _context.Districts.AddAsync(district, cancellation);
                    await _context.SaveChangesAsync(cancellation);

                    Ward ward = new Ward
                    {
                        Name = entity.NameWard.Trim(),
                        DistrictId = district.Id
                    };
                    await _context.Wards.AddAsync(ward, cancellation);
                    await _context.SaveChangesAsync(cancellation);

                    return Result.Success();
                }
            }
            catch (Exception ex)
            {
                return new Result(Domain.ResultStatus.Forbidden, "Đã xảy ra lỗi trong quá trình tạo địa chỉ.");
            }
        }

        public override Task<Result> Delete(int id, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        public override async Task<PagingModel<AdressMapping>> Get(AdressQuery query, CancellationToken cancellation)
        {
            // Lấy danh sách các tỉnh, huyện và xã không theo dõi để tránh load lại chúng từ context sau khi truy vấn
            var provinces = _context.Provinces.AsNoTracking();
            var districts = _context.Districts.AsNoTracking();
            var wards = _context.Wards.AsNoTracking();

            // Áp dụng bộ lọc từ AdressQuery vào truy vấn wards
            var filteredWards = wards.ApplyQuery(query, applyPagination: false);

            // Đếm tổng số bản ghi sau khi áp dụng bộ lọc
            var totalCount = await filteredWards.CountAsync(cancellation);

            // Áp dụng phân trang nếu cần
            var pagedWards = filteredWards.ApplyQuery(query, applyPagination: true);

            // Thực thi truy vấn và lấy dữ liệu
            var wardList = await pagedWards.ToListAsync(cancellation);

            // Ánh xạ kết quả từ thực thể cơ sở dữ liệu sang mô hình AdressMapping
            var data = wardList.Select(ward => new AdressMapping
            {
                NameWard = ward.Name,
                NameDisTrict = districts.FirstOrDefault(d => d.Id == ward.DistrictId)?.Name,
                NameProvince = districts.FirstOrDefault(d => d.Id == ward.DistrictId) != null ? provinces.FirstOrDefault(p => p.Id == districts.FirstOrDefault(d => d.Id == ward.DistrictId).ProvinceId) != null ? provinces.FirstOrDefault(p => p.Id == districts.FirstOrDefault(d => d.Id == ward.DistrictId).ProvinceId).Name : null : null
            }).ToList();

            // Trả về mô hình phân trang
            return new PagingModel<AdressMapping>(data, totalCount, query.Page ?? 1, query.PageSize ?? 10);
        }

        public override Task<AdressMapping?> Get(int id, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        public override Task<Result> Update(int id, UpdateAdress entity, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}
