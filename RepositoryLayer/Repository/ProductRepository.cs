using RepositoryLayer.IRepository;
using RepositoryLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    public class ProductRepository:IProductRepository
    {
        private readonly SandeepContext _context;
        public ProductRepository(SandeepContext context)
        {
            _context = context;
        }
        public List<Product> GetProducts()
        { 
            var pro = _context.Products.ToList();
            return pro;
        }
    }
}
