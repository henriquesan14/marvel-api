using Marvel.API.Entities;
using Marvel.API.Exceptions;
using Marvel.API.Infra.Repositories;
using Marvel.API.Services;
using Marvel.API.Utils;
using MediatR;

namespace Marvel.API.Commands
{
    public class AddFavoriteCharacterCommandHandler : IRequestHandler<AddFavoriteCharacterCommand, Character>
    {
        private readonly IMarvelRepository _repository;
        private readonly IMarvelApiService _service;

        public AddFavoriteCharacterCommandHandler(IMarvelRepository repository, IMarvelApiService service)
        {
            _repository = repository;
            _service = service;
        }

        public async Task<Character> Handle(AddFavoriteCharacterCommand request, CancellationToken cancellationToken)
        {
            var listFavorites = await _repository.GetFavoriteCharacters();
            if(listFavorites.Count() >= 5) {
                throw new MaximumFavoriteCharacterException("Already exists 5 favorites characters.");
            }
            var parametersApi = MarvelApiAuthenticator.GenerateApiParameters();
            var resultApi = await _service.GetCharactersById(parametersApi, request.Id);

            if (resultApi.Code == 404)
            {
                throw new NotFoundException($"Character with id {request.Id} not exists.");
            }

            var favoriteCharacterViewModel = resultApi.Data.Results[0];
            favoriteCharacterViewModel.IsFavorite = true;

            var result =  await _repository.AddFavoriteCharacter(favoriteCharacterViewModel);
            if(result == null)
            {
                throw new AlreadyFavoriteCharacterException($"Already favorite character with {request.Id}.");
            }
            return result;        
        }
    }
}
