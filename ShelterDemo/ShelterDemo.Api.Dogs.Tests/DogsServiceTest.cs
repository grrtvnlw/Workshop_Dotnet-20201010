using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ShelterDemo.Api.Dogs.Db;
using ShelterDemo.Api.Dogs.Profiles;
using ShelterDemo.Api.Dogs.Providers;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ShelterDemo.Api.Dogs.Tests
{
    public class DogsServiceTest
    {
        //public DogsServiceTest()
        //{
        //    CreateDogs(dbContext);
        //}

        [Fact]
        public async Task GetDogsReturnsAllDogsAsync()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DogsDbContext>().UseInMemoryDatabase(nameof(GetDogsReturnsAllDogsAsync))
                                                                      .Options;
            var dbContext = new DogsDbContext(options);

            CreateDogs(dbContext);

            var dogProfile = new DogProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(dogProfile));
            var mapper = new Mapper(configuration);

            var sut = new DogsProvider(dbContext, null, mapper);

            // Act
            var result = await sut.GetDogsAsync();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Dogs.Any());
            Assert.Null(result.ErrorMessage);
        }

        [Fact]
        public async Task GetDogReturnsDogsUsingValidId()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DogsDbContext>().UseInMemoryDatabase(nameof(GetDogReturnsDogsUsingValidId))
                                                                      .Options;
            var dbContext = new DogsDbContext(options);

            CreateDogs(dbContext);

            var dogProfile = new DogProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(dogProfile));
            var mapper = new Mapper(configuration);

            var sut = new DogsProvider(dbContext, null, mapper);
            Guid dogId = Guid.Parse("005c68b6-a5e9-4fff-b7d9-c88ca27c39f1");

            // Act
            var dog = await sut.GetDogAsync(dogId);

            // Assert
            Assert.True(dog.IsSuccess);
            Assert.NotNull(dog.Dog);
            Assert.True(dog.Dog.Id == dogId);
            Assert.Null(dog.ErrorMessage);
        }

        [Fact]
        public async Task GetDogReturnsNoDogsUsingNonExistingId()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DogsDbContext>().UseInMemoryDatabase(nameof(GetDogReturnsNoDogsUsingNonExistingId))
                                                                      .Options;
            var dbContext = new DogsDbContext(options);

            CreateDogs(dbContext);

            var dogProfile = new DogProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(dogProfile));
            var mapper = new Mapper(configuration);

            var sut = new DogsProvider(dbContext, null, mapper);
            Guid dogId = Guid.Parse("005c68b6-a5e9-4fff-b799-c88aa27c39f4");

            // Act
            var dog = await sut.GetDogAsync(dogId);

            // Assert
            Assert.False(dog.IsSuccess);
            Assert.Null(dog.Dog);
            Assert.NotNull(dog.ErrorMessage);
        }

        private void CreateDogs(DogsDbContext dbContext)
        {
            for (int i = 0; i < 10; i++)
            {
                dbContext.Dogs.Add(new Dog()
                { 
                    Id = i == 0 ? Guid.Parse("005c68b6-a5e9-4fff-b7d9-c88ca27c39f1") : Guid.NewGuid(),
                    Name = Guid.NewGuid().ToString()
                });
            }

            dbContext.SaveChanges();
        }
    }
}
