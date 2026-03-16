using RepositoryLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.IRepository
{
    public interface IAuthRepository
    {
        User GetUser(string username, string password);
    }
}
