using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductApi.Controllers;
using ProductApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ProductApi.Tests
{
    [TestClass]
    public class ProductsControllerTests
    {
        private ProductsController _controller;

        [TestInitialize]
        public void Setup()
        {
            ProductsController.ResetData();
            _controller = new ProductsController();
        }

        [TestMethod]
        public void GetProducts_ReturnsEmptyList()
        {
            var result = _controller.GetProducts().Result as OkObjectResult;
            var list = result.Value as System.Collections.Generic.IEnumerable<Product>;
            Assert.AreEqual(0, list.Count());
        }

        [TestMethod]
        public void CreateProduct_AddsProduct()
        {
            var product = new Product { Name = "Laptop", Price = 50000 };
            var result = _controller.CreateProduct(product).Result as CreatedAtActionResult;
            var created = result.Value as Product;
            Assert.AreEqual("Laptop", created.Name);
            Assert.AreEqual(50000, created.Price);
        }

        [TestMethod]
        public void GetProduct_ReturnsNotFound_WhenMissing()
        {
            var result = _controller.GetProduct(999);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void UpdateProduct_ReturnsNotFound_WhenMissing()
        {
            var result = _controller.UpdateProduct(999, new Product());
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteProduct_ReturnsNotFound_WhenMissing()
        {
            var result = _controller.DeleteProduct(999);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
