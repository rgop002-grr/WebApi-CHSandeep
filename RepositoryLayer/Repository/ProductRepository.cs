using Microsoft.EntityFrameworkCore;
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

        public  async Task<bool> AddProductAsync(Product product)
        {
            
            try
            {
                var res= _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            try
            {
                var exist = await _context.Products
                    .FirstOrDefaultAsync(x => x.Id == product.Id);
                if (exist == null) 
                    return false;
                exist.Name = product.Name;
                exist.Description = product.Description;
                exist.Category = product.Category;
                exist.Price = product.Price;
                exist.Quantity = product.Quantity;
                var res= await _context.SaveChangesAsync();
                return res > 0;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            try
            {
                if (id < 1)
                {
                    return false ;
                }
                var proid = await _context.Products.FindAsync(id);
                if (proid == null) return false;
                _context.Products.Remove(proid);
                var res = await _context.SaveChangesAsync();
                return res > 0;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task InsertBulkAsync(List<Product> products)
        {
            await _context.Products.AddRangeAsync(products);
            await _context.SaveChangesAsync();
        }

    }
}
