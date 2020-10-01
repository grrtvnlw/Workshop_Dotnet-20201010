using System;
using System.Threading.Tasks;

namespace ShelterDemo.Api.Search.Interfaces
{
    public interface ISearchService
    {
        Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(Guid dogId);
    }
}
