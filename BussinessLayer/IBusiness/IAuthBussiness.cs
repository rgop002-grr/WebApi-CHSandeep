using RepositoryLayer.ModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.IBusiness
{
    public interface IAuthBussiness
    {
        string Authenticate(LoginDto model);
    }
}
