using System;
using System.Collections;
using System.Collections.Generic;
using EndTerm.Controllers.Api;
using EndTerm.Models;
using EndTerm.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EndTermUnitTest
{
    public class CategoryControllerTest

    {

        [Fact] 
        public void GetAllCategoryTest()
        {
            var categoryList = new List<Category>
            {
                new Category {Id=1, Name = "Test"}
            };
            var repository = new Mock<ICategoryRepository>();
            repository.Setup(r => r.GetAllCategory()).Returns(categoryList);
            var categoryController = new CategoryController(repository.Object);
            var category = categoryController.GetAllCategories();

            Assert.IsAssignableFrom<IEnumerable<Category>>(category);
        }

        [Fact]
        public void GetCategoryByCorrectIdTest()
        {
            var category = new Category
            {
                Id = 1,
                Name = "Test Category"
            };
            var repository = new Mock<ICategoryRepository>();
            repository.Setup(r => r.GetCategory(1)).Returns(category);
            var categoryController = new CategoryController(repository.Object);
            var category2 = categoryController.GetCategory(1);
            
            Assert.IsType<OkObjectResult>(category2);
        }
        
        [Fact]
        public void GetCategoryByWrongIdTest()
        {
            var repository = new Mock<ICategoryRepository>();
            var categoryController = new CategoryController(repository.Object);
            var category2 = categoryController.GetCategory(-123);
            Assert.IsType<NotFoundObjectResult>(category2);
        }

    }
}