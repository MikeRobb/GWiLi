using GWiLi.EntityFramework.Extensions;
using System;
using System.Security.Cryptography;
using System.Text;

namespace GWiLi.EntityFramework
{
    public partial class User
    {
        public string GetDisplayName()
        {
            var result = string.Empty;
            bool addSpace = false;
            if(!string.IsNullOrEmpty(FirstName))
            {
                result = result.AddString(FirstName, addSpace);
                addSpace = true;
            }

            if (!string.IsNullOrEmpty(MiddleName))
            {
                result = result.AddString(MiddleName, addSpace);
                addSpace = true;
            }

            if (!string.IsNullOrEmpty(LastName))
            {
                result = result.AddString(LastName, addSpace);
                addSpace = true;
            }

            return result;
        }

        #region Password

        public bool HasBeenClaimed()
        {
            return Hash != null;
        }

        private const int SaltSize = 16;
        private const int HashSize = 20;
        private const int IterationCount = 1000;

        public static byte[] GenerateSalt()
        {
            using (var crypt = new RNGCryptoServiceProvider())
            {
                byte[] salt;
                crypt.GetBytes(salt = new byte[16]);
                return salt;
            }
        }

        public static byte[] GenerateHash(string password, byte[] salt)
        {
            var bytes = Encoding.Unicode.GetBytes(password);
            var dest = new byte[bytes.Length + salt.Length];
            Array.Copy(salt, 0, dest, 0, salt.Length);
            Array.Copy(bytes, 0, dest, salt.Length, bytes.Length);
            using (var rfc = new Rfc2898DeriveBytes(password, salt, IterationCount))
            {
                return rfc.GetBytes(HashSize);
            }
        }

        #endregion
    }
}