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
    public class OblastControllerTest

    {

        [Fact] 
        public void GetAllOblastTest()
        {
            var oblastList = new List<Oblast>
            {
                new Oblast {Id=1, Name = "Test"}
            };
            var repository = new Mock<IOblastRepository>();
            repository.Setup(r => r.GetAllOblasts()).Returns(oblastList);
            var oblastController = new OblastController(repository.Object);
            var oblast = oblastController.GetAllOblasts();

            Assert.IsAssignableFrom<IEnumerable<Oblast>>(oblast);
        }

        [Fact]
        public void GetOblastByCorrectIdTest()
        {
            var oblast = new Oblast
            {
                Id = 1,
                Name = "Test Oblast"
            };
            var repository = new Mock<IOblastRepository>();
            repository.Setup(r => r.GetOblast(1)).Returns(oblast);
            var oblastController = new OblastController(repository.Object);
            var result = oblastController.GetOblast(1);
            
            Assert.IsType<OkObjectResult>(result);
        }
        
        [Fact]
        public void GetOblastByWrongIdTest()
        {
            var repository = new Mock<IOblastRepository>();
            var oblastController = new OblastController(repository.Object);
            var result = oblastController.GetOblast(-123);
            Assert.IsType<NotFoundObjectResult>(result);
        }

    }
}