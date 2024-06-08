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
        private readonly ApplicationServiceBase<AdressMapping, AdressQuery, CreateAdress, UpdateAdress> _context;

        public AddressController(ApplicationServiceBase<AdressMapping, AdressQuery, CreateAdress, UpdateAdress> context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<Result> CreateAdress(CreateAdress entity, CancellationToken cancellationToken)
        {
            return await _context.Create(entity, cancellationToken);
        }
        [HttpPut]
        public async Task<Result> UpdateAdress(UpdateAdress entity, CancellationToken cancellationToken)
        {
            return await _context.Updatemore(entity, cancellationToken);
        }
        [HttpGet]
        public async Task<PagingModel<AdressMapping>> GetAdress([FromQuery] AdressQuery query, CancellationToken cancellationToken)
        {
            return await _context.Get(query, cancellationToken);
        }
    }
}
