using RepositoryLayer.ModelDto;
using RepositoryLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BussinessLayer.IBusiness
{
    public interface IProductBussiness
    {
        List<ProductDto> GetProducts();
        Task<bool> AddProductAsync(ProductDto product);
        Task<bool> UpdateProductAsync(ProductDto product);
        Task<bool> DeleteProductAsync(int id);
        Task<string> UploadExcelAsync(IFormFile file);
    }
}
