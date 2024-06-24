using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHoc.Application.Helper
{
    public class RandomEmailConfirmCode
    {
        private static Random random = new Random();

        public static string RandomCode(int length)
        {
            string code = string.Empty;

            for (int i = 0; i < length; i++)
            {
                code = String.Concat(code, random.Next(10).ToString());
            }

            return code;
        }
    }
}
