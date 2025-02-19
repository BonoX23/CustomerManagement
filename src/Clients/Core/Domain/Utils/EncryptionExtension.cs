using System.Security.Cryptography;
using System.Text;

namespace Domain.Utils
{
    public static class EncryptionExtension
    {
        public static string GenerateHashPassword(this string login, string password)
        {
            var md5Hash = MD5.Create();
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes($"#tgs#{password}##{login}#tgs#"));

            var sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2").ToUpper());
            }

            return sBuilder.ToString();
        }
    }
}
