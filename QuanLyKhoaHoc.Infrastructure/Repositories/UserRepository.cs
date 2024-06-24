using QuanLyKhoaHoc.Domain.Entities;
using QuanLyKhoaHoc.Domain.Interfaces;
using QuanLyKhoaHoc.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHoc.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task CreateUser(User user)
        {
            try
            {
                await AddAsync(user);
            }
            catch
            {
                throw;
            }
        }

        public async Task<User?> FindUser(string userName)
        {
            try
            {
                return await FindAsync(x => x.FullName == userName);
            }
            catch
            {
                throw;
            }
        }

        public async Task<User?> FindUser(int userId)
        {
            try
            {
                return await FindAsync(x => x.Id == userId);
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateUser(User user)
        {
            try
            {
                await UpdateAsync(user);
            }
            catch
            {
                throw;
            }
        }
    }

}
