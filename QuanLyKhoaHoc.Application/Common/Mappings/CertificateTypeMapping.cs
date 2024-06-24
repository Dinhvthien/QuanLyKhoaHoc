using QuanLyKhoaHoc.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHoc.Application.Common.Mappings
{
    public class CertificateTypeMapping
    {
        public string Name { get; set; } = "";
    }
    public class CertificateTypeQuery : QueryModel { }

    public class CreateCertificateType
    {
        public string Name { get; set; } = "";
    }

    public class UpdateCertificateType
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
