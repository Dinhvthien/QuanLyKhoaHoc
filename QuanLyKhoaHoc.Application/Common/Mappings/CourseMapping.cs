﻿using QuanLyKhoaHoc.Application.Common.Models;
using QuanLyKhoaHoc.Domain.Entities;

namespace QuanLyKhoaHoc.Application.Common.Mappings
{
    public class CourseMapping
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string Introduce { get; set; } = default!;

        public string ImageCourse { get; set; } = default!;

        public string Code { get; set; } = default!;

        public decimal Price { get; set; }
        public List<SubjectMapping> Subject { get; set; }


        public int TotalCourseDuration { get; set; }

        public int NumberOfStudent { get; set; }

        public int NumberOfPurchases { get; set; }
    }

    public class CourseQuery : QueryModel { }

    public class CourseCreate
    {
        public string Name { get; set; } = default!;

        public string Introduce { get; set; } = default!;

        public string ImageCourse { get; set; } = default!;

        public string Code { get; set; } = default!;

        public decimal Price { get; set; }
        public List<int> SubjectId { get; set; }


        public int TotalCourseDuration { get; set; }

        public int NumberOfStudent { get; set; }

        public int NumberOfPurchases { get; set; }
    }

    public class CourseUpdate
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string Introduce { get; set; } = default!;

        public string ImageCourse { get; set; } = default!;

        public int CreatorId { get; set; }

        public string Code { get; set; } = default!;

        public decimal Price { get; set; }
        public List<int> Subject { get; set; }
        public int TotalCourseDuration { get; set; }

        public int NumberOfStudent { get; set; }

        public int NumberOfPurchases { get; set; }
    }
}
