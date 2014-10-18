using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace CollegeBuffer.DAL.Special
{
    public class TokenGenerator
    {
        public static string EncryptMd5(string key, string type)
        {
            var cypher = MD5.Create();

            // Calculate MD5 hash from input
            var inputBytes = Encoding.ASCII.GetBytes(key);
            var hash = cypher.ComputeHash(inputBytes);

            // Convert byte array to HEX string
            var sb = new StringBuilder();

            foreach (var part in hash)
            {
                sb.Append(part.ToString(type));
            }

            return sb.ToString();
        }

        public static string EncryptMd5(string key)
        {
            return EncryptMd5(key, "X2");
        }

        public static string ExtractSpecialHashKey(object obj)
        {
            return obj == null ? null : obj.GetHashCode().ToString(CultureInfo.InvariantCulture);
        }
    }
}