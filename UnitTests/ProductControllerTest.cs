using BussinessLayer.IBusiness;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RepositoryLayer.ModelDto;
using RepositoryLayer.Models;
using WebApplication2.Controllers;

namespace UnitTests
{
    public class ProductControllerTest
    {

        [Fact]
        public void GetProductDetails_ReturnsNotFound_WhenEmpty()
        {
            var mockService = new Mock<IProductBussiness>();
            var mockLogger = new Mock<ILogger<ProductController>>();

            mockService.Setup(x => x.GetProducts())
                       .Returns(new List<ProductDto>());

            var controller = new ProductController(mockService.Object, mockLogger.Object);

            var result = controller.GetProductDetails();

            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}