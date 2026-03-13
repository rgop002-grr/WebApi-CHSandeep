using RepositoryLayer.ModelDto;
using RepositoryLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.IBusiness
{
    public interface IProductBussiness
    {
        List<ProductDto> GetProducts();
    }
}
