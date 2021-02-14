using System;
using System.Net;
using System.Text.RegularExpressions;

namespace PWCheck
{
    internal static class Program
    {
        private static void Main()
        {
            bool loop = true;
            while (loop)
            {
                Console.WriteLine("Please enter password to check.");
                string password = Console.ReadLine();
                string Result = PasswordCheck(password);
                Console.WriteLine(Result);

                if (!UtilsConsole.Confirm("Test different password?"))
                {
                    loop = false;
                }
            }
        }

        private static String PasswordCheck(string password)
        {
            string PWHash = UtilsCrypto.Hash(password);
            PWHash = PWHash.Replace("-", "").ToUpper();
            string front = PWHash[..5];
            string back = PWHash[5..];

            string Uri = "https://api.pwnedpasswords.com/range/" + front;

            var client = new WebClient();

            string body = client.DownloadString(Uri);

            string pattern = back + ":([0-9]*)";

            Regex rx = new(pattern);

            Match match = rx.Match(body);

            if (match.Success)
            {
                return $"Your password was found {match.Groups[1]} times";
            }
            else
            {
                return "Your password was not found, that's great!";
            }
        }
    }
}
