using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoaHoc.Application.Common.Interfaces;
using QuanLyKhoaHoc.Application.Common.Mappings;
using QuanLyKhoaHoc.Application.Services;

namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticalController : ControllerBase
    {
        private readonly IStatisticalService _statisticalService;


        public StatisticalController(IStatisticalService statisticalService)
        {
            _statisticalService = statisticalService;
        }

        [HttpGet]
        public async Task<StatisticalMapping> Get()
        {
            return await _statisticalService.GetStatisticalMappingAsync();
        }
        [HttpGet("month")]
        public async Task<List<StatisticalMonthMapping>> GetMonth(DateTime startDate, DateTime endDate)
        {
            return await _statisticalService.GetStatisticalMounthMappingAsync(startDate, endDate);
        }
        [HttpGet("gettop5courseinmonth")]
        public async Task<List<CourseStatiscalMapping>> Gettop5CourseMonth()
        {
            return await _statisticalService.GettopCourseinMonth();
        }
        [HttpGet("gettop5courseinrage")]
        public async Task<List<CourseStatiscalMapping>> Gettop5CourseRage(DateTime startdate, DateTime enddate)
        {
            return await _statisticalService.GettopCourseinRange(startdate,enddate);
        }
    }
}
