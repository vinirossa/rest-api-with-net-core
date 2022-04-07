using RestAPI.Data.VOs;
using RestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RestAPI.Repository
{
    public class UserRepository : IRepository<User>
    {
        public UserRepository(MyDbContext context)
        {
            _context = context;
        }

        private MyDbContext _context;

        public User ValidateCredentials(UserVO user) 
        {
            var password = ComputeHash(user.Password, new SHA256CryptoServiceProvider());
            return _context.Users.FirstOrDefault(u => (u.Username == user.Username) && (u.Password == user.Password));
        }

        private string ComputeHash(string input, SHA256CryptoServiceProvider algorithm)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }

        public List<User> FindAll()
        {
            return _context.Users.ToList();
        }

        public User FindById(long id)
        {
            return _context.Users.SingleOrDefault(p => p.Id.Equals(id));
        }

        public User Create(User user)
        {
            try
            {
                _context.Add(user);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return null;
            }

            return user;
        }

        public User Update(User user)
        {
            if (!Exists(user.Id))
                return null;

            var result = _context.Users.SingleOrDefault(p => p.Id.Equals(user.Id));

            _context.Entry(result).CurrentValues.SetValues(user);
            _context.SaveChanges();

            return result;
        }

        public void Delete(long id)
        {
            if (!Exists(id))
                return;

            var result = _context.Users.SingleOrDefault(p => p.Id.Equals(id));

            _context.Users.Remove(result);
            _context.SaveChanges();
        }

        public bool Exists(long id)
        {
            return _context.Users.Any(p => p.Id.Equals(id));
        }
    }
}
