using Microsoft.AspNetCore.Mvc;
using ShelterDemo.Api.Dogs.Interfaces;
using System;
using System.Threading.Tasks;

namespace ShelterDemo.Api.Dogs.Controllers
{
    [ApiController]
    [Route("api/dogs")]
    public class DogsController : ControllerBase
    {
        private readonly IDogsProvider dogsProvider;

        public DogsController(IDogsProvider dogsProvider)
        {
            this.dogsProvider = dogsProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetDogsAsync()
        {
            var result = await dogsProvider.GetDogsAsync();
            if (result.IsSuccess)
            {
                return Ok(result.Dogs);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDogAsync(Guid id)
        {
            var result = await dogsProvider.GetDogAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Dog);
            }
            return NotFound();
        }
    }
}
