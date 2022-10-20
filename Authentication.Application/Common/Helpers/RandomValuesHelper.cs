using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Common.Helpers
{
    public static class RandomValuesHelper
    {
        public static string GenerateRandomNumber()
        {
            Random r = new Random();
            return r.Next(1000000, 9999999).ToString();
        }

        public static string RandomString()
        {
            var random = new Random();
            const string chars = "01234567890123456789012345678901234567890123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }


    public static class ForgetCodeValuesHelper
    {
        public static string GenerateRandomNumber()
        {
            Random r = new Random();
            return r.Next(100000, 999999).ToString();
        }

        public static string RandomString()
        {
            var random = new Random();
            const string chars = "0123456789012345678901234567890123456";
            return new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
