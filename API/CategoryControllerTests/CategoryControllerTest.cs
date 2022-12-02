using AutoMapper;
using GSC_FinalProject.Controllers;
using GSC_FinalProject.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Cryptography.X509Certificates;

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
            var result = target.GetAll();

            //result.Should().BeOfType<OkObjectResult>();
        }
    }
}