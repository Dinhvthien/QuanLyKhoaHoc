using QuanLyKhoaHoc.Application.Common.Mappings;
using QuanLyKhoaHoc.Application.Common.Models;

namespace QuanLyKhoaHoc.Application.Common.Interfaces
{
    public interface ISubjectService
    {
        public Task<PagingModel<SubjectMapping>> GetSubjects(SubjectQuery query, CancellationToken cancellation);

        public Task<SubjectMapping?> GetSubject(int id, CancellationToken cancellation);

        public Task<Result> CreateSubject(SubjectCreate entity, CancellationToken cancellation);

        public Task<Result> UpdateSubject(int id, SubjectUpdate entity, CancellationToken cancellation);

        public Task<Result> DeleteSubject(int id, CancellationToken cancellation);
    }
}
