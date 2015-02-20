using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace PManager
{
    public class PasswordManager
    {
        private string salt = "breezytelephone";

        private string GetMD5Hash(string passwd)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            byte[] inputBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(passwd);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString().ToLower();
        }

        private string GetMD5Hash(string password, bool salt, string value)
        {
            if (salt)
            {
                return GetMD5Hash(value + password);
            }
            else
            {
                return GetMD5Hash(password);
            }
        }

        public static void Main(String[] args)
        {
            PasswordManager pm = new PasswordManager();
            Console.WriteLine(pm.GetMD5Hash("String", true, pm.salt));

            HDManager hdmanager = new HDManager();

            int userid = (new Random()).Next();

            string location = "C:\\TEMP\\";
            location += userid + ".vhd";

            long diskSize = (long)Math.Pow(1024, 3);

            Console.WriteLine(hdmanager.CreateNtfsDrive(diskSize, location, "Disk", true));
        }

    }
}
