using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using ProductApp.Controllers;
using ProductApp.DataTransferObjects;
using ProductApp.Entities;
using ProductApp.Repositories;
using Xunit;

namespace ProductApp.Test
{
    public class ProductUnitTests
    {
        [Fact]
        public void Delete_Tag()
        {
            // Arrange
            var mapper = CreateMapperObject().CreateMapper();

            var mockRepo = new Mock<IAppProductRepository>();

            mockRepo.Setup(repo => repo.GetTagAsync(It.IsAny<Guid>()))
                .ReturnsAsync(GetSingleTag());

            var controller = new TagController(mapper, mockRepo.Object);

            // Act
            var result = controller.DeleteTag(It.IsAny<Guid>());

            // Assert
            var productOutput = Assert.IsType<NoContentResult>(result.Result);
            var model = Assert.IsAssignableFrom<NoContentResult>(
                productOutput);
            Assert.Equal(204, model.StatusCode);
        }
        [Fact]
        public void Delete_Product()
        {
            // Arrange
            var mapper = CreateMapperObject().CreateMapper();
            var mockRepo = new Mock<IAppProductRepository>();

            mockRepo.Setup(repo => repo.GetProductAsync(It.IsAny<Guid>()))
                .ReturnsAsync(GetSingleProduct());

            var controller = new ProductController(mapper, mockRepo.Object);

            // Act
            var result = controller.DeleteProduct(It.IsAny<Guid>());

            // Assert
            var productOutput = Assert.IsType<NoContentResult>(result.Result);
            var model = Assert.IsAssignableFrom<NoContentResult>(
                productOutput);
            Assert.Equal(204, model.StatusCode);
        }

        [Fact]
        public void Create_Product()
        {
            // Arrange
            var mapper = CreateMapperObject().CreateMapper();

            var mockRepo = new Mock<IAppProductRepository>();

            mockRepo.Setup(repo => repo.AddProduct(It.IsAny<Product>()));
            mockRepo.Setup(repo => repo.AddTag(It.IsAny<Tag>()));

            var controller = new ProductController(mapper, mockRepo.Object);

            var inputDto = new ProductCreateDto
            {
                Name = "sport car",
                Price = 8000.9,
                Tags = new List<TagCreationDto>
                {
                    new TagCreationDto{Value = "blue"}
                }
            };

            // Act
            var result = controller.CreateProduct(inputDto);

            // Assert

            var model = result.Result as CreatedAtRouteResult;
            var productToCheck = model?.Value as Product;

            Assert.Equal("sport car", productToCheck?.Name);
        }
        [Fact]
        public void Create_Tag()
        {
            // Arrange

            var mapper = CreateMapperObject().CreateMapper();

            var mockRepo = new Mock<IAppProductRepository>();

            mockRepo.Setup(repo => repo.GetProductAsync(It.IsAny<Guid>()))
                .ReturnsAsync(GetSingleProduct());
            mockRepo.Setup(repo => repo.AddTag(It.IsAny<Tag>()));

            var controller = new TagController(mapper, mockRepo.Object);

            var inputDto = new TagCreationDto
            {
                Value = "blue"
            };

            // Act
            var result = controller.CreateTag(It.IsAny<Guid>(), inputDto);

            // Assert

            var tagOutput = Assert.IsType<OkResult>(result.Result);
            var model = Assert.IsAssignableFrom<OkResult>(
                tagOutput);
            Assert.Equal(200, model.StatusCode);
        }

        [Fact]
        public void Get_Product_ById()
        {
            // Arrange

            var mapper = CreateMapperObject().CreateMapper();

            var mockRepo = new Mock<IAppProductRepository>();

            mockRepo.Setup(repo => repo.GetProductAsync(It.IsAny<Guid>()))
                .ReturnsAsync(GetSingleProduct());

            var controller = new ProductController(mapper, mockRepo.Object);

            // Act
            var result = controller.GetProduct(It.IsAny<Guid>());

            // Assert
            var productOutput = Assert.IsType<ProductOutputDto>(result.Result);
            var model = Assert.IsAssignableFrom<ProductOutputDto>(
                productOutput);
            Assert.Equal(new Guid("8aa42e54-bdec-4666-b9a5-39976b92d697"), model.ProductId);
        }

        [Fact]
        public void Get_All_Products_With_Search()
        {
            // Arrange
            var mapper = CreateMapperObject().CreateMapper();

            var mockRepo = new Mock<IAppProductRepository>();

            mockRepo.Setup(repo => repo.GetAllProducts())
                .Returns(GetTestSessions());

            var controller = new ProductController(mapper, mockRepo.Object);

            // Act
            var result = controller.GetAllProduct(GetSearchParameter("shoes", "blue"));

            // Assert
            var productOutput = Assert.IsType<List<ProductOutputDto>>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ProductOutputDto>>(
                productOutput);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void Update_Products()
        {
            // Arrange
            var mapper = CreateMapperObject().CreateMapper();

            var mockRepo = new Mock<IAppProductRepository>();

            mockRepo.Setup(repo => repo.GetProductAsync(It.IsAny<Guid>()))
                .ReturnsAsync(GetSingleProduct());

            var controller = new ProductController(mapper, mockRepo.Object);

            var inputDto = new ProductUpdateDto
            {
                Name = "updatedName",
                Price = 100
            };

            // Act
            var result = controller.UpdateProduct(It.IsAny<Guid>(), inputDto);

            // Assert
            var model = result.Result as CreatedAtRouteResult;
            var productToCheck = model?.Value as Product;

            Assert.Equal("updatedName", productToCheck?.Name);
        }

        private IQueryable<Product> GetTestSessions()
        {
            var sessions = new List<Product>();
            sessions.Add(new Product()
            {
                ProductId = new Guid("910d9c32-fc0e-47d2-a259-33dafa51170b"),
                IsDeleted = false,
                Name = "running shoes",
                Price = 80.2,
                Tags = new List<Tag>
                {
                    new Tag
                    {
                        ProductId = new Guid("910d9c32-fc0e-47d2-a259-33dafa51170b"),
                        IsDeleted = false,
                        Value = "blue"
                    },
                    new Tag
                    {
                        ProductId = new Guid("910d9c32-fc0e-47d2-a259-33dafa51170b"),
                        IsDeleted = false,
                        Value = "37"
                    },
                }
            });
            sessions.Add(new Product()
            {
                ProductId = new Guid("8aa42e54-bdec-4666-b9a5-39976b92d697"),
                IsDeleted = false,
                Name = "baseball shoes",
                Price = 109.5,
                Tags = new List<Tag>
                {
                    new Tag
                    {
                        ProductId = new Guid("8aa42e54-bdec-4666-b9a5-39976b92d697"),
                        IsDeleted = false,
                        Value = "red"
                    },
                    new Tag
                    {
                        ProductId = new Guid("8aa42e54-bdec-4666-b9a5-39976b92d697"),
                        IsDeleted = false,
                        Value = "37"
                    },
                }
            });
            sessions.Add(new Product()
            {
                ProductId = new Guid("ba88cd4d-9d8e-4160-9343-f3a2b0d95535"),
                IsDeleted = false,
                Name = "soccer shoes",
                Price = 109.5,
                Tags = new List<Tag>
                {
                    new Tag
                    {
                        ProductId = new Guid("ba88cd4d-9d8e-4160-9343-f3a2b0d95535"),
                        IsDeleted = false,
                        Value = "blue"
                    },
                    new Tag
                    {
                        ProductId = new Guid("ba88cd4d-9d8e-4160-9343-f3a2b0d95535"),
                        IsDeleted = false,
                        Value = "38"
                    },
                }
            });
            return sessions.AsQueryable();
        }

        private SearchParameters GetSearchParameter(string name, string tag)
        {
            return new SearchParameters
            {
                Name = name,
                Tag = tag

            };
        }

        private Product GetSingleProduct()
        {
            return new Product
            {
                ProductId = new Guid("8aa42e54-bdec-4666-b9a5-39976b92d697"),
                IsDeleted = false,
                Name = "baseball shoes",
                Price = 109.5,
                Tags = new List<Tag>
                {
                    new Tag
                    {
                        ProductId = new Guid("8aa42e54-bdec-4666-b9a5-39976b92d697"),
                        IsDeleted = false,
                        Value = "red"
                    },
                    new Tag
                    {
                        ProductId = new Guid("8aa42e54-bdec-4666-b9a5-39976b92d697"),
                        IsDeleted = false,
                        Value = "37"
                    },
                }
            };
        }
        private Tag GetSingleTag()
        {
            return new Tag
            {
                ProductId = Guid.NewGuid(),
                IsDeleted = false,
                Value = "red"
            };
        }

        private MapperConfiguration CreateMapperObject()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
        }
    }
}
