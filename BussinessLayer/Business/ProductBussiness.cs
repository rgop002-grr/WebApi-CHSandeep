using BussinessLayer.IBusiness;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.IRepository;
using RepositoryLayer.ModelDto;
using RepositoryLayer.Models;
using RepositoryLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Business
{
    //
    public class ProductBussiness:IProductBussiness
    {
        private readonly IProductRepository _repo;
        public ProductBussiness(IProductRepository repo)
        {
            _repo = repo;
        }
        public List<ProductDto> GetProducts()
        {
            var pro = _repo.GetProducts();
            var result = pro.Select(p=> new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Catogery   = p.Category,
                Price = p.Price,
                Quantity = p.Quantity,
            }).ToList();
            return result;
        }
    }
}
