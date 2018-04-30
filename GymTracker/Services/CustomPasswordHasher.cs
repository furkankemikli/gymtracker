using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GymTracker.Services
{
    public class CustomPasswordHasher<TUser> : IPasswordHasher<TUser> where TUser : class
    {
        public string HashPassword(TUser user,string password)
        {
            return GetHash(password);
        }

        public PasswordVerificationResult VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword)
        {
            string hashOfProvided = GetHash(providedPassword);
            if(hashOfProvided.Equals(hashedPassword))
                return PasswordVerificationResult.Success;
            else return PasswordVerificationResult.Failed;
        }

        private static string GetHash(string password)
        {
            byte[] data = Encoding.ASCII.GetBytes(password);
            byte[] hash;
            using (SHA512 shaM = new SHA512Managed())
            {
                hash = shaM.ComputeHash(data);
            }
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
