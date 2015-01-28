using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobLib
{
    public class RandomStringGenerator
    {
        public static string Generate()
        {
            const int size = 8;
            const string characteres = "abcdefghijklmnopqrstuvwxyz1234567890";

            string randomString = String.Empty;

            var random = new Random();

            for (int i = 0; i < size; i++)
            {
                int next = random.Next(0, characteres.Length - 1);

                randomString += characteres[next];
            }

            return randomString;
        }
    }
}
