using System.Security.Cryptography;
using System.Text;

namespace CoreProjectLib.Extensions
{
    public static class StringExtensions
    {
        public static string GetDigits(this string input) 
        {
            return new string(input.Where(c => char.IsDigit(c)).ToArray());
        }

        public static string ToSHA256(this string text)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(text);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }

    }
}
