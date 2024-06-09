using QuanLyKhoaHoc.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHoc.Application.Common.Mappings
{
    public class SubjectDetailMapping
    {
        public int Id { get; set; }

        public int SubjectId { get; set; }

        public string Name { get; set; }

        public bool IsFinished { get; set; }

        public string LinkVideo { get; set; }

        public bool IsActive { get; set; }

    }
    public class SubjectDetailQuery : QueryModel { }

    public class SubjectDetailCreate
    {

        public string Name { get; set; }
        public int SubjectId { get; set; }

        public bool IsFinished { get; set; }

        public string LinkVideo { get; set; }

        public bool IsActive { get; set; }

    }
    public class SubjectDetailUpdate
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsFinished { get; set; }

        public string LinkVideo { get; set; }

        public bool IsActive { get; set; }

    }


}
