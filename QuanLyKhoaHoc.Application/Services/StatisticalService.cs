using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyKhoaHoc.Application.Common.Interfaces;
using QuanLyKhoaHoc.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHoc.Application.Services
{
    public class StatisticalService : IStatisticalService
    {
        private readonly IApplicationDbContext _context;
        public StatisticalService(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<StatisticalMapping> GetStatisticalMappingAsync()
        {
            var getRes = await _context.Users.Where(c => c.CreateTime == DateTime.Now).ToListAsync();
            var getResCourse = await _context.RegisterStudys.Where(c => c.RegisterTime == DateTime.Now).ToListAsync();
            var getlistCourseID = getResCourse.GroupBy(c => c.CourseId).ToList();
            decimal totalRevenue = 0;
            foreach (var courseGroup in getlistCourseID)
            {
                var courseId = courseGroup.Key;
                var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
                if (course != null)
                {
                    totalRevenue += course.Price * courseGroup.Count();
                }
            }
            StatisticalMapping statisticalMapping = new StatisticalMapping();
            statisticalMapping.NumberRegister = getRes.Count();
            statisticalMapping.NumberRegisterCourse = getResCourse.Count();
            statisticalMapping.revenue = totalRevenue;
            return statisticalMapping;
        }
        public async Task<List<StatisticalMonthMapping>> GetStatisticalMounthMappingAsync(DateTime startDate, DateTime endDate)
        {
            List<StatisticalMonthMapping> stlist = new List<StatisticalMonthMapping>();

            // Lặp qua từng tháng trong khoảng thời gian
            for (DateTime date = startDate; date <= endDate; date = date.AddMonths(1))
            {
                var startOfMonth = new DateTime(date.Year, date.Month, 1);
                var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

                var usersInMonth = await _context.Users
                    .Where(c => c.CreateTime >= startOfMonth && c.CreateTime <= endOfMonth)
                    .ToListAsync();

                var registerStudysInMonth = await _context.RegisterStudys
                    .Where(c => c.RegisterTime >= startOfMonth && c.RegisterTime <= endOfMonth)
                    .ToListAsync();

                var groupedByCourse = registerStudysInMonth.GroupBy(c => c.CourseId);

                decimal totalRevenue = 0;
                foreach (var courseGroup in groupedByCourse)
                {
                    var courseId = courseGroup.Key;
                    var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
                    if (course != null)
                    {
                        totalRevenue += course.Price * courseGroup.Count();
                    }
                }

                stlist.Add(new StatisticalMonthMapping
                {
                    Month = date.Month,
                    NumberRegister = usersInMonth.Count(),
                    NumberRegisterCourse = registerStudysInMonth.Count(),
                    revenue = totalRevenue
                });
            }

            return stlist;
        }

        public async Task<List<CourseStatiscalMapping>> GettopCourseinMonth()
        {
            var stlist = new List<CourseStatiscalMapping>();
            var currentDate = DateTime.Now;
            var startOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
            var getResCourse = await _context.RegisterStudys
                .Where(c => c.RegisterTime >= startOfMonth && c.RegisterTime <= endOfMonth)
                .ToListAsync();

            var courseIds = getResCourse.Select(c => c.CourseId).Distinct();

            foreach (var courseId in courseIds)
            {
                var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
                if (course != null)
                {
                    var quantity = getResCourse.Count(c => c.CourseId == courseId);
                    stlist.Add(new CourseStatiscalMapping
                    {
                        CourseId = courseId,
                        Quantity = quantity,
                        imageCourse = course.ImageCourse,
                        CourseName = course.Name,
                    });
                }
            }

            stlist = stlist.OrderByDescending(g => g.Quantity).Take(5).ToList();

            return stlist;
        }

        public async Task<List<CourseStatiscalMapping>> GettopCourseinRange(DateTime startDate, DateTime endDate)
        {
            var stlist = new List<CourseStatiscalMapping>();

            var getResCourse = await _context.RegisterStudys
                .Where(c => c.RegisterTime >= startDate && c.RegisterTime <= endDate)
                .ToListAsync();

            var courseIds = getResCourse.Select(c => c.CourseId).Distinct();

            foreach (var courseId in courseIds)
            {
                var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
                if (course != null)
                {
                    var quantity = getResCourse.Count(c => c.CourseId == courseId);
                    stlist.Add(new CourseStatiscalMapping
                    {
                        CourseId = courseId,
                        Quantity = quantity,
                        imageCourse = course.ImageCourse,
                        CourseName = course.Name,
                    });
                }
            }

            stlist = stlist.OrderByDescending(g => g.Quantity).Take(5).ToList();

            return stlist;
        }

    }
}
