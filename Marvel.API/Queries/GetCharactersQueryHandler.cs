using Marvel.API.Entities;
using Marvel.API.Infra.Repositories;
using Marvel.API.Services;
using Marvel.API.Utils;
using Marvel.API.ViewModels;
using MediatR;

namespace Marvel.API.Queries
{
    public class GetCharactersQueryHandler : IRequestHandler<GetCharactersQuery, ResponseAPIViewModel<Character>>
    {
        private readonly IMarvelApiService _service;
        private readonly IMarvelRepository _repository;

        public GetCharactersQueryHandler(IMarvelApiService service, IMarvelRepository repository)
        {
            _service = service;
            _repository = repository;
        }

        public async Task<ResponseAPIViewModel<Character>> Handle(GetCharactersQuery request, CancellationToken cancellationToken)
        {
            var parametersApi = MarvelApiAuthenticator.GenerateApiParameters(request.Limit, request.Offset, request.Name);

            var listFavorites = await _repository.GetFavoriteCharacters();
            var resultApi = await _service.GetCharacters(parametersApi);

            listFavorites.ToList().ForEach(item =>
            {
                resultApi.Data.Results.Add(item);
            });

            resultApi.Data.Results = resultApi.Data.Results
                .OrderByDescending(character => character.IsFavorite)
                        .ThenBy(character => character.Name)
                            .DistinctBy(c => c.Id)
                            .Take(request.Limit).ToList();


            return resultApi;
        }

    }
}
