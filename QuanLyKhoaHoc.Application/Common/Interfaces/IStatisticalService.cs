using QuanLyKhoaHoc.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHoc.Application.Common.Interfaces
{
    public interface IStatisticalService
    {
        public Task<StatisticalMapping> GetStatisticalMappingAsync();
    }
}
