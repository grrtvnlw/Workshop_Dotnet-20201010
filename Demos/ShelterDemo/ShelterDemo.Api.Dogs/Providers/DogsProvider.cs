using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShelterDemo.Api.Dogs.Db;
using ShelterDemo.Api.Dogs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShelterDemo.Api.Dogs.Providers
{
    public class DogsProvider : IDogsProvider
    {
        private readonly DogsDbContext dbContext;
        private readonly ILogger<DogsProvider> logger;
        private readonly IMapper mapper;

        public DogsProvider(DogsDbContext dbContext, ILogger<DogsProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Dog> Dogs, string ErrorMessage)> GetDogsAsync()
        {
            try
            {
                logger?.LogInformation("Querying dogs");

                var dogs = await dbContext.Dogs.ToListAsync();
                if (dogs != null && dogs.Any())
                {
                    logger?.LogInformation($"{dogs.Count} dog(s) found");

                    var result = mapper.Map<IEnumerable<Db.Dog>, IEnumerable<Models.Dog>>(dogs);

                    return (true, result, null);
                }

                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());

                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.Dog Dog, string ErrorMessage)> GetDogAsync(Guid id)
        {
            try
            {
                logger?.LogInformation("Querying dogs");

                var dog = await dbContext.Dogs.FirstOrDefaultAsync(dog => dog.Id == id);
                if (dog != null)
                {
                    logger?.LogInformation("Dog found");

                    var result = mapper.Map<Db.Dog, Models.Dog>(dog);

                    return (true, result, null);
                }

                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());

                return (false, null, ex.Message);
            }
        }

        private void SeedData()
        {
            if (!dbContext.Dogs.Any())
            {
                dbContext.Dogs.Add(new Db.Dog()
                { 
                    Id = Guid.Parse("005c68b6-a5e9-4fff-b7d9-c88ca27c39f9"),
                    Name = "Pluto"
                });
                dbContext.Dogs.Add(new Db.Dog()
                {
                    Id = Guid.Parse("3f229d19-3820-4b1c-afe7-fce462ccaf7f"),
                    Name = "Patra"
                });
                dbContext.Dogs.Add(new Db.Dog()
                {
                    Id = Guid.Parse("0eb4edbf-f8da-4059-a94b-a2d3f16694cc"),
                    Name = "Domm"
                });

                dbContext.SaveChanges();
            }
        }
    }
}
