using System;
using System.Text;
using System.Security.Cryptography;



namespace CSCore
{
    public class CSEncryption
    {
        static CSEncryption() { }

        public static string CreateSalt(int size)
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number.
            return Convert.ToBase64String(buff);
        }

        public static string CreatePasswordHash(string pwd, string salt, string hashAlgorithm)
        {
            HashAlgorithm hash;

            string saltAndPwd = String.Concat(pwd, salt);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(saltAndPwd);

                
             switch (hashAlgorithm.ToUpper())
            {
                case "SHA1":
                    hash = new SHA1Managed();
                    break;

                case "SHA256":
                    hash = new SHA256Managed();
                    break;

                case "SHA384":
                    hash = new SHA384Managed();
                    break;

                case "SHA512":
                    hash = new SHA512Managed();
                    break;

                default:
                    hash = new MD5CryptoServiceProvider();
                    break;
            }           


            byte[] hashWithSaltBytes = hash.ComputeHash(plainTextBytes);

            return Convert.ToBase64String(hashWithSaltBytes); 
        }

    }
}





