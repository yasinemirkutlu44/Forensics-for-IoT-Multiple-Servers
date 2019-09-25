using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Forensic.Handler
{
    public class HashCode
    {
        public string PasswordHass(string password)
        {
            SHA1 sha1 = SHA1.Create();
            Byte[] hashdata = sha1.ComputeHash(Encoding.Default.GetBytes(password));
            StringBuilder value = new StringBuilder();
            for(int i=0; i < hashdata.Length ; i++)
            {
                value.Append(hashdata[i].ToString());
            }
            return value.ToString();
        }
    }
}
