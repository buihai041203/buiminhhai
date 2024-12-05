using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystem
{
    internal class PasswordHasher
    {
        // Phương thức để băm mật khẩu
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create()) // Sử dụng thuật toán SHA256
            {
                // Chuyển đổi mật khẩu thành mảng byte
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Băm mật khẩu
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                // Chuyển đổi mảng byte băm thành chuỗi hex để dễ lưu trữ
                StringBuilder hashString = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashString.Append(b.ToString("x2"));
                }

                return hashString.ToString(); // Trả về mật khẩu đã băm dưới dạng chuỗi hex
            }
        }
    }
}
