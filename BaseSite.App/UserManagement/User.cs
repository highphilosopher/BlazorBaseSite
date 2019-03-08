using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BaseSite.App.UserManagement
{
    public class User
    {

        public String Email { get; set; }

        public HashSet<String> Permissions { get; set; }

        public string PasswordHash { get; set; }

        public String PasswordSalt { get; set; }

        public User(string email, string password, HashSet<String> permissions, string salt = null)
        {
            if(salt != null)
            {
                PasswordSalt = salt;
            }
            else
            {
                PasswordSalt = BCrypt.Net.BCrypt.GenerateSalt();
            }
            PasswordHash = HashPassword(password);
            Email = email;
            Permissions = permissions;
        }

        public User()
        {
            Permissions = new HashSet<string>();
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, PasswordSalt);
        }

        public Boolean SetPassword(string password)
        {
            if(!CheckPassword(password))
            {
                PasswordHash = HashPassword(password);
                return true;
            }
            return false;
        }

        public Boolean CheckPassword(string password)
        {
            string hash = HashPassword(password);
            if(hash == PasswordHash)
            {
                return true;
            }
            return false;
        }

    }
}
