using ShelterDemo.Api.Dogs.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShelterDemo.Api.Dogs.Interfaces
{
    public interface IDogsProvider
    {
        Task<(bool IsSuccess, IEnumerable<Dog> Dogs, string ErrorMessage)> GetDogsAsync();

        Task<(bool IsSuccess, Dog Dog, string ErrorMessage)> GetDogAsync(Guid id);
    }
}
