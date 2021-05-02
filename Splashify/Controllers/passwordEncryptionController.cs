using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;

namespace Splashify.Controllers
{
    public class passwordEncryptionController
    {
        private string salt = null;
        private string hash = null;
        private SHA256CryptoServiceProvider encryptionObj = null;

        public passwordEncryptionController()
        {

        }

        public void setSalt(string userSalt)
        {
            salt = userSalt;
        }

        public void generateSalt()
        {
            var saltGenerator = new RNGCryptoServiceProvider();
            int salt_max_size = 64;
            byte[] byte_salt = new byte[salt_max_size];
            saltGenerator.GetNonZeroBytes(byte_salt);

            salt = byte_salt.ToString();
        }

        public string generateHash(string input, bool return_hash)
        {
            byte[] byteString = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(input + salt));

            var sBuilder = new StringBuilder();

            for (int i = 0; i < byteString.Length; i++)
            {
                sBuilder.Append(byteString[i].ToString("X2"));
            }

            if (return_hash)
            {
                return sBuilder.ToString();
            }
            else
            {
                hash = sBuilder.ToString();
                return null;
            }

        }

        public bool compareHash(string user_password)
        {
            if (salt != null && hash != null)
            {
                string generated_hash = generateHash(user_password, true);
                if (generated_hash == hash)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Encryption object not setup correctly.");
                return false;
            }

        }

        public string getSalt()
        {
            return salt;
        }

        public string getHasg()
        {
            return hash;
        }
    }
}
