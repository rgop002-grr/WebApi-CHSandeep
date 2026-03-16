using RepositoryLayer.IRepository;
using RepositoryLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    public class AuthRepository:IAuthRepository
    {
        private readonly SandeepContext _context;

        public AuthRepository(SandeepContext context)
        {
            _context = context;
        }

        public User GetUser(string username, string password)
        {
            return _context.Users
                .FirstOrDefault(x => x.Username == username && x.Password == password);
        }
    }
}
