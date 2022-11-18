using AutoMapper;
using DirectorInformation.API.Controllers;
using DirectorInformation.API.Entities;
using DirectorInformation.API.Profiles;
using DirectorInformation.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DirectorInformationTests
{
    public class DirectorControllerTest : IDisposable
    {
        public void Dispose() { }

        private DirectorsController _controller;
        private HttpContext _httpContext;
        public DirectorControllerTest()
        {
            var MockRepository = new Mock<IDirectorInformationRepository>();
            var mapperConf = new MapperConfiguration(c => c.AddProfile<DirectorProfile>());
            var Mapper = new Mapper(mapperConf);

            _httpContext = new DefaultHttpContext();

            _controller = new DirectorsController(MockRepository.Object, Mapper);
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = _httpContext
            };
        }

        [Fact]
        public async Task GetDirectorsTest()
        {
            //arrange

            //act
            var directors = await _controller.GetDirectors(null, null);

            //assert
            Assert.True(directors != null);
        }

        [Fact]
        public async Task GetSerieTest()
        {
            //arrange

            //act
            var serie = await _controller.GetDirector(3, false);

            //assert
            Assert.True(serie != null);
        }
    }
}