using System.Security.Cryptography;
using System.Text;

namespace BE_RestaurantManagement.Helpers
{
    public static class HashHelper
    {
        public static string HmacSHA256(string data, string key)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower(); // Convert to hex string
            }
        }
    }
}
