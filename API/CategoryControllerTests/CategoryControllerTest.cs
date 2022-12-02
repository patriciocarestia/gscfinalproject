using AutoMapper;
using GSC_FinalProject.Controllers;
using GSC_FinalProject.Data;
using GSC_FinalProject.Dto;
using GSC_FinalProject.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Cryptography.X509Certificates;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace CategoryControllerTests
{
    public class CategoryControllerTest
    {
        private Mock<IMapper> mapper = new Mock<IMapper>();
        private Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
        private CategoryController target;

        public CategoryControllerTest()
        {
            target = new CategoryController(uow.Object, mapper.Object);
        }

        [Fact]
        public void GetAll_ShouldReturnOk()
        {
            Arrange_GetCategories();

            var result = target.GetAll();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void GetAll_ShouldReturnCategories()
        {
            Arrange_GetCategories();

            var result = target.GetAll();
            result.As<OkObjectResult>().Value.Should().BeOfType<CategoryDTO[]>();
        }

        [Fact]
        public void GetCategory_ShouldReturnOk()
        {
            Arrange_GetCategories();

            var result = target.GetCategory(It.IsAny<int>());
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void GetCategory_ShouldReturnCategory()
        {
            Arrange_GetCategories();

            var result = target.GetCategory(It.IsAny<int>());
            result.As<OkObjectResult>().Value.Should().BeOfType<CategoryDTO>();
        }

        [Fact]
        public void Add_ShouldReturnOk()
        {
            var category = Arrange_GetCategories().First();

            var result = target.Add(category);
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void Add_ShouldReturnCategory()
        {

            var category = Arrange_GetCategories().First();

            var result = target.Add(category);
            result.As<OkObjectResult>().Value.Should().BeOfType<CategoryDTO>();
        }

        [Fact]
        public void Update_ShouldReturnOk()
        {
            var category = Arrange_GetCategories().First();

            var result = target.Update(It.IsAny<int>(), category);
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void Update_ShouldReturnCategory()
        {

            var category = Arrange_GetCategories().First();

            var result = target.Update(It.IsAny<int>(), category);
            result.As<OkObjectResult>().Value.Should().BeOfType<CategoryDTO>();
        }

        [Fact]
        public void Delete_ShouldReturnOk()
        {
            Arrange_GetCategories().First();

            var result = target.Delete(It.IsAny<int>());
            result.Should().BeOfType<OkResult>();
        }

        private CategoryDTO[] Arrange_GetCategories()
        {
            var categories = new List<Category>() { new Category
                { Id=1, Description="category", CreationDate= DateTime.UtcNow }
            };
            uow.Setup(u => u.CategoriesRepository.GetAll()).Returns(categories);
            uow.Setup(u => u.CategoriesRepository.GetById(It.IsAny<int>())).Returns(categories.First<Category>);

            var categoriesDTOs = new CategoryDTO[]{ new CategoryDTO
                { Description="category" }
            };
            mapper.Setup(m => m.Map<CategoryDTO[]>(categories)).Returns(categoriesDTOs);
            mapper.Setup(m => m.Map<CategoryDTO>(categories.First())).Returns(categoriesDTOs.First());
            mapper.Setup(m => m.Map<Category>(categoriesDTOs.First())).Returns(categories.First());

            return categoriesDTOs;
        }
    }
}