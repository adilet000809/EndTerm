using System;
using System.Collections;
using System.Collections.Generic;
using EndTerm.Controllers.Api;
using EndTerm.Models;
using EndTerm.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EndTermUnitTest
{
    public class CityControllerTest

    {

        [Fact] 
        public void GetAllCityTest()
        {
            var cityList = new List<City>
            {
                new City {Id=1, Name = "Test", OblastId = 1, Oblast = new Oblast {Id = 1, Name = "Test Oblast"}}
            };
            var repository = new Mock<ICityRepository>();
            var repository2 = new Mock<IOblastRepository>();
            repository.Setup(r => r.GetAllCities()).Returns(cityList);
            var cityController = new CityController(repository.Object, repository2.Object);
            var result = cityController.GetAllCities();

            Assert.IsAssignableFrom<IEnumerable<City>>(result);
        }

        [Fact]
        public void GetOblastByCorrectIdTest()
        {
            var city = new City
            {
                Id=1, Name = "Test", OblastId = 1, Oblast = new Oblast {Id = 1, Name = "Test Oblast"}
            };
            var repository = new Mock<ICityRepository>();
            var repository2 = new Mock<IOblastRepository>();
            repository.Setup(r => r.GetCity(1)).Returns(city);
            var cityController = new CityController(repository.Object, repository2.Object);
            var result = cityController.GetCity(1);
            
            Assert.IsType<OkObjectResult>(result);
        }
        
        [Fact]
        public void GetOblastByWrongIdTest()
        {
            var repository = new Mock<ICityRepository>();
            var repository2 = new Mock<IOblastRepository>();
            var cityController = new CityController(repository.Object, repository2.Object);
            var result = cityController.GetCity(-123);
            Assert.IsType<NotFoundObjectResult>(result);
        }

    }
}