using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoaHoc.Application.Common.Mappings;
using QuanLyKhoaHoc.Application.Common;
using QuanLyKhoaHoc.Application.Common.Models;

namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectDetailController : ControllerBase
    {
        private readonly ApplicationServiceBase<SubjectDetailMapping, SubjectDetailQuery, SubjectDetailCreate, SubjectDetailUpdate> _context;

        public SubjectDetailController(ApplicationServiceBase<SubjectDetailMapping, SubjectDetailQuery, SubjectDetailCreate, SubjectDetailUpdate> context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<PagingModel<SubjectDetailMapping>> GetSubjectdetails([FromQuery] SubjectDetailQuery query, CancellationToken cancellationToken)
        {
            return await _context.Get(query, cancellationToken);
        }
        [HttpGet("{id}")]
        public async Task<SubjectDetailMapping?> GetSubjectdetail(int id, CancellationToken cancellationToken)
        {
            return await _context.Get(id, cancellationToken);
        }

        [HttpPost]
        public async Task<Result> CreateSubjectDetail(SubjectDetailCreate entity, CancellationToken cancellationToken)
        {
            return await _context.Create(entity, cancellationToken);
        }

        [HttpPut]
        public async Task<Result> UpdateSubjectDetail(int id, SubjectDetailUpdate entity, CancellationToken cancellationToken)
        {
            return await _context.Update(id, entity, cancellationToken);
        }

        [HttpDelete]
        public async Task<Result> DeleteSubjectDêtail(int id, CancellationToken cancellationToken)
        {
            return await _context.Delete(id, cancellationToken);
        }
    }
}
