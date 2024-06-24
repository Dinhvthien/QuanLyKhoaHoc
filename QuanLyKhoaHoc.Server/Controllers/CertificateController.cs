using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoaHoc.Application.Common.Mappings;
using QuanLyKhoaHoc.Application.Common.Models;
using QuanLyKhoaHoc.Application.Common;

namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        private readonly ApplicationServiceBase<CertificateMapping, CertificateQuery, CreateCertificate, UpdateCertificate> _context;

        public CertificateController(ApplicationServiceBase<CertificateMapping, CertificateQuery, CreateCertificate, UpdateCertificate> context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<Result> CreateAdress(CreateCertificate entity, CancellationToken cancellationToken)
        {
            return await _context.Create(entity, cancellationToken);
        }
        [HttpGet]
        public async Task<PagingModel<CertificateMapping>> GetCertificate([FromQuery] CertificateQuery query, CancellationToken cancellationToken)
        {
            return await _context.Get(query, cancellationToken);
        }
        [HttpPut]
        public async Task<Result> UpdateCertificate(int Id, UpdateCertificate query, CancellationToken cancellationToken)
        {
            return await _context.Update(Id, query, cancellationToken);
        }
        [HttpDelete]
        public async Task<Result> DeleteCertificate(int id, CancellationToken cancellationToken)
        {
            return await _context.Delete(id, cancellationToken);
        }
    }
}
