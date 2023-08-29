using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Controllers;
using RunGroopWebApp.Data.Enum;
using RunGroopWebApp.Helpers;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;
using RunGroopWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RunGroopWebApp.Tests.Controllers
{
    public class ClubControllerTests
    {
        private ClubController _clubController;
        private IClubRepository _clubRepository;
        private IPhotoService _photoService;
        private IHttpContextAccessor _httpContextAccessor;


        private List<Club> fakeClubs;
        private Club fakeClub;
        private Address address1;
        private Address address2;

        public ClubControllerTests()
        {
            // Dependency
            _clubRepository = A.Fake<IClubRepository>();
            _photoService = A.Fake<IPhotoService>();
            _httpContextAccessor = A.Fake<IHttpContextAccessor>();

            // SUT
            _clubController = new ClubController(_clubRepository, _photoService);

            // Addresses
            address1 = new Address
            {
                Id = 1, 
                Street = "15 Yemen Road",
                City = "Sanaa",
                State = "Yemen", 
                ZipCode = 1819101
            };
            address2 = new Address
            {
                Id = 1,
                Street = "15 Yemen Road",
                City = "Yemen",
                State = "NewYork",
                ZipCode = 1812666
            };

            // Fake clubs
            fakeClubs = new List<Club>
            {
                new Club
                {
                    Id = 1,
                    Title = "Club1",
                    Description = "Description1",
                    Image = "Image1.jpg",
                    AddressId = 1,
                    Address = address1,
                    ClubCategory = ClubCategory.Trail,
                    AppUserId = "User1"
                },
                new Club
                {
                    Id = 2,
                    Title = "Club2",
                    Description = "Description2",
                    Image = "Image2.jpg",
                    AddressId = 2,
                    Address = address2,
                    ClubCategory = ClubCategory.Endurance,
                    AppUserId = "User2"
                }
            };

            fakeClub = fakeClubs[0];
        }

        [Fact]
        public void ClubController_Index_ReturnsSuccess()
        {
            // Arrange
            A.CallTo(() => _clubRepository.GetAll()).Returns(fakeClubs);

            // Act
            var result = _clubController.Index();

            // Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void ClubController_Detail_ReturnsSuccess()
        {
            // Arrange
            var id = 1;
            A.CallTo(() => _clubRepository.GetByIdAsync(id)).Returns(fakeClub);

            // Act
            var result = _clubController.DetailClub(id, "RunningClub");

            // Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }

    }
}
