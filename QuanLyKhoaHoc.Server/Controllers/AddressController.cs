using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoaHoc.Application.Common.Mappings;
using QuanLyKhoaHoc.Application.Common;
using QuanLyKhoaHoc.Application.Common.Models;

namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly ApplicationServiceBase<AdressMapping, AdressQuery, createAdress, UpdateAdress> _context;

        public AddressController(ApplicationServiceBase<AdressMapping, AdressQuery, createAdress, UpdateAdress> context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<Result> CreateAdress(createAdress entity, CancellationToken cancellationToken)
        {
            return await _context.Create(entity, cancellationToken);
        }
        [HttpGet]
        public async Task<PagingModel<AdressMapping>> GetAdress([FromQuery] AdressQuery query, CancellationToken cancellationToken)
        {
            return await _context.Get(query, cancellationToken);
        }
    }
}
