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
using System.Text;
using System.Threading.Tasks;

namespace DirectorInformationTests
{
    public class FilmControllerTest
    {
        private FilmController _controller;
        private HttpContext _httpContext;

        public FilmControllerTest()
        {
            var MockRepository = new Mock<IDirectorInformationRepository>();

            var mapperConf = new MapperConfiguration(c => c.AddProfile<FilmProfile>());
            var mapper = new Mapper(mapperConf);

            _httpContext = new DefaultHttpContext();

            _controller = new FilmController(MockRepository.Object, mapper);

            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = _httpContext,
            };
        }

        [Fact]
        public async void GetFilmsTest()
        {
            //arrange
            int directorId = 3;

            //act
            var films = await _controller.GetFilms(directorId);

            //assert
            Assert.NotNull(films);
        }


        [Fact]
        public async void GetFilmTest()
        {
            //arrange
            int directorId = 1;
            int filmId = 1;

            //act
            var film = await _controller.GetFilm(directorId, filmId);

            //assert
            Assert.NotNull(film);
        }
    }
}