using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoaHoc.Application.Common.Mappings;
using QuanLyKhoaHoc.Application.Common.Models;
using QuanLyKhoaHoc.Application.Common;

namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateTypeController : ControllerBase
    {
        private readonly ApplicationServiceBase<CertificateTypeMapping, CertificateTypeQuery, CreateCertificateType, UpdateCertificateType> _context;

        public CertificateTypeController(ApplicationServiceBase<CertificateTypeMapping, CertificateTypeQuery, CreateCertificateType, UpdateCertificateType> context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<Result> Create(CreateCertificateType entity, CancellationToken cancellationToken)
        {
            return await _context.Create(entity, cancellationToken);
        }
        [HttpGet]
        public async Task<PagingModel<CertificateTypeMapping>> GetSubjects([FromQuery] CertificateTypeQuery query, CancellationToken cancellationToken)
        {
            return await _context.Get(query, cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<CertificateTypeMapping?> GetSubject(int id, CancellationToken cancellationToken)
        {
            return await _context.Get(id, cancellationToken);
        }


        [HttpPut]
        public async Task<Result> UpdateSubject(int id, UpdateCertificateType entity, CancellationToken cancellationToken)
        {
            return await _context.Update(id, entity, cancellationToken);
        }

        [HttpDelete]
        public async Task<Result> DeleteSubject(int id, CancellationToken cancellationToken)
        {
            return await _context.Delete(id, cancellationToken);
        }
    }
}
