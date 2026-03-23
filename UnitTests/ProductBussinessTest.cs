using BussinessLayer.Business;
using Moq;
using RepositoryLayer.IRepository;
using RepositoryLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class ProductBussinessTest
    {
        [Fact]
        public void GetProducts_ReturnsProducts()
        {
            // Arrange
            var mockRepo = new Mock<IProductRepository>();

            mockRepo.Setup(r => r.GetProducts())
                .Returns(new List<Product>
            {
                new Product { Id = 1, Name = "Laptop" },
                new Product { Id = 2, Name = "Mobile" }
            });

            var service = new ProductBussiness(mockRepo.Object);

            // Act
            var result = service.GetProducts();

            // Assert
            Assert.Equal(2, result.Count);
        }
    }
}
