using Marvel.API.Entities;
using Marvel.API.InputModels;
using Marvel.API.ViewModels;
using Refit;

namespace Marvel.API.Services
{
    public interface IMarvelApiService
    {
        [Get("/v1/public/characters")]
        Task<ResponseAPIViewModel<Character>> GetCharacters(RequestApiParameter parameters);
        [Get("/v1/public/characters/{id}")]
        Task<ResponseAPIViewModel<Character>> GetCharactersById(RequestApiParameter parameters, int id);
    }
}
