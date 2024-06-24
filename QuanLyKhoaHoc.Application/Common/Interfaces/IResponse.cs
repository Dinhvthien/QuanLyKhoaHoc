using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHoc.Application.Common.Interfaces
{
    public interface IResponse
    {
        public Task<IResponse> NoContent(int Status, string Message);

        public Task<IResponse> Content(int Status, string Message, object Data);
    }
}
