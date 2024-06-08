using QuanLyKhoaHoc.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHoc.Application.Common.Mappings
{
    public class AdressMapping
    {
        public string NameProvince { get; set; } = "";
        public string NameDisTrict { get; set; } = "";
        public string NameWard { get; set; } = "";
    }
    public class AdressQuery : QueryModel { }

    public class CreateAdress
    {
        public string NameProvince { get; set; } = "";
        public string NameDisTrict { get; set; } = "";
        public string NameWard { get; set; } = "";
    }

    public class UpdateAdress
    {
        public int IdProvince { get; set; }
        public int IdDisTrict { get; set; }
        public int IDWard { get; set; }
        public string NameProvince { get; set; } = "";
        public string NameDisTrict { get; set; } = "";
        public string NameWard { get; set; } = "";
    }
}
