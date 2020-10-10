using System;
using System.Threading.Tasks;

namespace ShelterDemo.Api.Search.Interfaces
{
    public interface IDogsService
    {
        Task<(bool IsSuccess, dynamic Dog, string ErrorMessage)> GetDogAsync(Guid id);
    }
}
