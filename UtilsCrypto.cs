using System;
using System.Text;
using System.Security.Cryptography;

namespace PWCheck
{
    internal static class UtilsCrypto
    {
        public static string Hash(string stringToHash)
        {
            using var sha1 = new SHA1Managed();
            return BitConverter.ToString(sha1.ComputeHash(Encoding.UTF8.GetBytes(stringToHash)));
        }
    }
}
