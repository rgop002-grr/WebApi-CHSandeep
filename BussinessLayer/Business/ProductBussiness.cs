using BussinessLayer.IBusiness;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
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
            var result = pro.Select(p => new ProductDto
            {
                Id=p.Id,
                Name = p.Name,
                Description = p.Description,
                Catogery = p.Category,
                Price = p.Price,
                Quantity = p.Quantity,
            }).ToList();
            return result;
        }
        public async Task<bool> AddProductAsync(ProductDto product)
        {
            try
            {
                // Map DTO to Entity
                var pro = new Product
                {
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description,
                    Category=product.Catogery,
                    Quantity=product.Quantity,
                    Sku = Guid.NewGuid().ToString(),
                };
                var res= await _repo.AddProductAsync(pro);
                return res;
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UpdateProductAsync(ProductDto product)
        {
            try
            {

                if (product == null)
                    throw new Exception("Product data is required");

                if (product.Id <= 0)
                    throw new Exception("Invalid Product ID");

                var pro = new Product
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Category = product.Catogery,
                    Price = product.Price,
                    Quantity = product.Quantity,
                };
                
                var res = await _repo.UpdateProductAsync(pro);
                return res;

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
                    return false;
                }
                var prores = await _repo.DeleteProductAsync(id);
                
                return prores;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<string> UploadExcelAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return "No file uploaded";

            var products = new List<Product>();
            ExcelPackage.License.SetNonCommercialPersonal("Sandeep");

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        try
                        {
                            var product = new Product
                            {
                                Name = worksheet.Cells[row, 1].Text,
                                Description = worksheet.Cells[row, 2].Text,
                                Category = worksheet.Cells[row, 3].Text,
                                Price = decimal.TryParse(worksheet.Cells[row, 4].Text, out var price) ? price : 0,
                                Quantity = int.TryParse(worksheet.Cells[row, 5].Text, out var qty) ? qty : 0,
                                Sku = Guid.NewGuid().ToString(),
                            };

                            if (string.IsNullOrWhiteSpace(product.Name))
                                continue;

                            products.Add(product);
                        }
                        catch
                        {
                            // skip invalid row
                            continue;
                        }
                    }
                }
            }

            await _repo.InsertBulkAsync(products);

            return $"{products.Count} records inserted successfully";
        }
    }
}
