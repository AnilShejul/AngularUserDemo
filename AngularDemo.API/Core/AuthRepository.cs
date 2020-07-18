using System;
using System.Threading.Tasks;
using AngularDemo.API.Data;
using AngularDemo.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularDemo.API.Core
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;

        }

        public async Task<bool> UserExist(string EmailAddress)
        {
           if(await _context.WebUser.AnyAsync(x=>x.EmailAddress==EmailAddress))
           return true;

           return false;
        }

        public async Task<WebUser> Login(string EmailAddress, string Password)
        {
           var user=await _context.WebUser.FirstOrDefaultAsync(x=>x.EmailAddress==EmailAddress);
           if(user==null)
           return null;

           if(!VerifyPasswordHash(Password,user.PasswordHash,user.PasswordSalt))
           return null;

           return user; 

        }

        private bool VerifyPasswordHash(string Password, byte[] PasswordHash, byte[] PasswordSalt)
        {
            using (var hmac=new System.Security.Cryptography.HMACSHA512(PasswordSalt))
           {

              var computedHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
              for(var i=0;i<computedHash.Length;i++)
              {
                  if(computedHash[i]!=PasswordHash[i])
                  return false;
              }

           }
           return true;
        }

        public async Task<WebUser> Register(WebUser User, string Password)
        {
            byte [] passwordHash,passwordSalt;
            CreatePasswordHash(Password,out passwordHash,out passwordSalt);
            User.PasswordHash=passwordHash;
            User.PasswordSalt=passwordSalt;

          await _context.WebUser.AddAsync(User);
         await _context.SaveChangesAsync();
         return User;

        }

        private void CreatePasswordHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
           using (var hmac=new System.Security.Cryptography.HMACSHA512())
           {
                PasswordSalt=hmac.Key;
                PasswordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));

           }
        }
    }
}