using QuanLyKhoaHoc.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuanLyKhoaHoc.Application.Common.Models
{
    public class Response : IResponse
    {
        public int Status { get; set; }
        public string? Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object? Data { get; set; }

        public async Task<IResponse> NoContent(int Status, string Message)
        {
            this.Status = Status;
            this.Message = Message;
            return await Task.FromResult(this);
        }

        public async Task<IResponse> Content(
            int Status,
            string Message,
            object Data
        )
        {
            this.Status = Status;
            this.Message = Message;
            this.Data = Data;
            return await Task.FromResult(this);
        }
    }
}
