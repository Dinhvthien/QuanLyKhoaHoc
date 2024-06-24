using QuanLyKhoaHoc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHoc.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public Task CreateUser(User user);

        public Task<User?> FindUser(string userName);

        public Task<User?> FindUser(int userId);

        public Task UpdateUser(User user);
    }
}
