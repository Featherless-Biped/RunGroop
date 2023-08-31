using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Data.Enum;
using RunGroopWebApp.Models;
using RunGroopWebApp.Repository;


namespace RunGroopWepApp.NTests.Repository
{
    public class ClubRepositoryTests
    {
        private async Task<ApplicationDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Clubs.CountAsync() <= 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Clubs.Add(
                        new Club()
                        {
                            Title = "Club1",
                            Description = "Description1",
                            Image = "Image1.jpg",
                            Address = new Address
                            {
                                Street = "15 Yemen Road",
                                City = "Yemen",
                                State = "Yemen",
                                ZipCode = 1819101
                            },
                            ClubCategory = ClubCategory.Trail,
                            AppUserId = "User1"
                        });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }
        [Test]
        public async Task ClubRepository_Add_ReturnsBool()
        {
            //Arrange
            var club = new Club()
            {
                Title = "Club1",
                Description = "Description1",
                Image = "Image1.jpg",
                Address = new Address
                {
                    Street = "15 Yemen Road",
                    City = "Yemen",
                    State = "Yemen",
                    ZipCode = 1819101
                },
                ClubCategory = ClubCategory.Trail,
                AppUserId = "User1"
            };
            var dbContext = await GetDbContext();
            //dbContext.Clubs.AsNoTracking();
            var clubRepository = new ClubRepository(dbContext);

            //Act
            var result = clubRepository.Add(club);

            //Assert
            result.Should().BeTrue();
        }
        [Test]
        public async Task ClubRepository_GetByIdAsync_ReturnsBool()
        {
            //Arrange
            var id = 1;
            var dbContext = await GetDbContext();
            var clubRepository = new ClubRepository(dbContext);

            //Act
            var result = clubRepository.GetByIdAsync(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<Club>>();
        }
    }
}
