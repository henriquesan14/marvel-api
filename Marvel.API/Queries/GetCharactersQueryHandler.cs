using Marvel.API.Infra.Repositories;
using Marvel.API.InputModels;
using Marvel.API.Services;
using Marvel.API.Utils;
using Marvel.API.ViewModels;
using MediatR;

namespace Marvel.API.Queries
{
    public class GetCharactersQueryHandler : IRequestHandler<GetCharactersQuery, ResponseAPIViewModel<Character>>
    {
        private readonly IMarvelApiService _service;
        private readonly IConfiguration _configuration;
        private readonly IMarvelRepository _repository;

        public GetCharactersQueryHandler(IMarvelApiService service, IConfiguration configuration, IMarvelRepository repository)
        {
            _service = service;
            _configuration = configuration;
            _repository = repository;
        }

        public async Task<ResponseAPIViewModel<Character>> Handle(GetCharactersQuery request, CancellationToken cancellationToken)
        {
            var ts = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            var publicKey = _configuration["MarvelApiSettings:PublicKey"];
            var privateKey = _configuration["MarvelApiSettings:PrivateKey"];
            var parameters = ts + privateKey + publicKey;
            var hash = parameters.ToMD5Hash();
            var parametersApi = new RequestApiParameter
            {
                ApiKey = publicKey!,
                Hash = hash,
                Ts = ts,
                Name = request.Name,
                Offset = request.Offset,
                Limit = request.Limit,
            };
            var listFavorites = await _repository.GetFavoriteCharacters();
            var resultApi = await _service.GetCharacters(parametersApi);

            var list = new List<Character>();

            if (listFavorites.Any())
            {
                foreach (var apiCharacter in resultApi.Data.Results)
                {
                    var favoriteCharacter = listFavorites.FirstOrDefault(c => c.Id == apiCharacter.Id);
                    if (favoriteCharacter != null)
                    {
                        apiCharacter.IsFavorite = true;
                    }
                    list.Add(apiCharacter!);
                }
                resultApi.Data.Results = list
                    .OrderByDescending(character => character.IsFavorite)
                        .ThenBy(character => character.Name);
            }


            return resultApi;
        }

    }
}
