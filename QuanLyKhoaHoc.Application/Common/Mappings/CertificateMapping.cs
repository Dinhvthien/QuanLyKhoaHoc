using QuanLyKhoaHoc.Application.Common.Models;
using QuanLyKhoaHoc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHoc.Application.Common.Mappings
{
    public class CertificateMapping
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public string Image { get; set; } = default!;
        public List<CertificateType> CertificateType { get; set; } = default!;

    }

    public class CertificateQuery : QueryModel { }

    public class CreateCertificate
    {
        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public string Image { get; set; } = default!;
        public List<int> CertificateTypeId { get; set; }
    }

    public class UpdateCertificate
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public string Image { get; set; } = default!;
        public int CertificateTypeId { get; set; }
    }

}
