using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Application.Helpers
{
    public static class PasswordCompute
    {

        public static string PasswordHashing(string password)
        {

            using (SHA256 hashAlgorithm = SHA256.Create())
            {
                //Use a array of bytes
                byte[] binaryData = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();

                //Generate a StringBuilder + the array concatenation
                for (int i = 0; i < binaryData.Length; i++)
                {
                    builder.Append(binaryData[i].ToString("x2"));
                }

                return builder.ToString();
            }

        }


    }

}
