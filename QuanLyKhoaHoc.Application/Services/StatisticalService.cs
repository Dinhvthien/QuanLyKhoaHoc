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
            var getRes = await _context.Users.Where(c=>c.CreateTime == DateTime.Now).ToListAsync();
            var getResCourse = await _context.RegisterStudys.Where(c => c.RegisterTime == DateTime.Now).ToListAsync();
            var getlistCourseID = getResCourse.GroupBy(c=>c.CourseId).ToList();
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
    }
}
