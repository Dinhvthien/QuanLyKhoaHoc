using Microsoft.AspNetCore.Mvc;
using QuanLyKhoaHoc.Application.Common.Interfaces;
using QuanLyKhoaHoc.Application.Common.Mappings;
using QuanLyKhoaHoc.Application.Common.Models;

namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _context;

        public SubjectController(ISubjectService context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<PagingModel<SubjectMapping>> GetSubjects([FromQuery] SubjectQuery query, CancellationToken cancellationToken)
        {
            return await _context.GetSubjects(query, cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<SubjectMapping?> GetSubject(int id, CancellationToken cancellationToken)
        {
            return await _context.GetSubject(id, cancellationToken);
        }

        [HttpPost]
        public async Task<Result> CreateSubject(SubjectCreate entity, CancellationToken cancellationToken)
        {
            return await _context.CreateSubject(entity, cancellationToken);
        }

        [HttpPut]
        public async Task<Result> UpdateSubject(int id, SubjectUpdate entity, CancellationToken cancellationToken)
        {
            return await _context.UpdateSubject(id, entity, cancellationToken);
        }

        [HttpDelete]
        public async Task<Result> DeleteSubject(int id, CancellationToken cancellationToken)
        {
            return await _context.DeleteSubject(id, cancellationToken);
        }
    }
}
