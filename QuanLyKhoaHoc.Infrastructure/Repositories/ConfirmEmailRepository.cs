using Microsoft.EntityFrameworkCore;
using QuanLyKhoaHoc.Domain.Entities;
using QuanLyKhoaHoc.Domain.Interfaces;
using QuanLyKhoaHoc.Infrastructure.Data;

namespace QuanLyKhoaHoc.Infrastructure.Repositories
{
    public class ConfirmEmailRepository : Repository<ConfirmEmail>,
        IConfirmEmailRepository
    {
        public ConfirmEmailRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ConfirmEmailUseCode(int userId, string confirmCode)
        {
            ConfirmEmail? confirmEmail = await Query(x =>
                 x.UserId == userId && x.ConfirmCode == confirmCode
             )
             .Include(x => x.User)
             .SingleOrDefaultAsync();

            if (confirmEmail == null)
            {
                return false;
            }

            if (confirmEmail.ExpiryTime < DateTime.Now)
            {
                return false;
            }

            confirmEmail.IsConfirm = true;

            if (confirmEmail.User != null)
            {
                confirmEmail.User.IsActive = true;
            }

            await UpdateAsync(confirmEmail);

            return true;
        }
    }
}
