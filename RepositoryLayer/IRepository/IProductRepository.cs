using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Models;

namespace RepositoryLayer.IRepository
{
     public interface IProductRepository
    {
         List<Product> GetProducts();
    }
}
