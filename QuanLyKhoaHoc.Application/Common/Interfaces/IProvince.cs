using QuanLyKhoaHoc.Application.Common.Mappings;
using QuanLyKhoaHoc.Application.Common.Models;

namespace QuanLyKhoaHoc.Application.Common.Interfaces
{
    public interface IProvince
    {
        public Task<List<ProvinceMapping>> GetProvinces(ProvinceQuery query);

        public Task<ProvinceMapping> GetProvince(int id);

        public Task<Result> CreateProvince(ProvinceCreate entity);

        public Task<Result> UpdateProvince(ProvinceUpdate entity);

        public Task<Result> DeleteProvince(int id);
    }
}
