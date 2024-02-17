using Marvel.API.Infra.Repositories;
using MediatR;

namespace Marvel.API.Commands
{
    public class AddFavoriteCharacterCommandHandler : IRequestHandler<AddFavoriteCharacterCommand, int>
    {
        private readonly IMarvelRepository _repository;

        public AddFavoriteCharacterCommandHandler(IMarvelRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(AddFavoriteCharacterCommand request, CancellationToken cancellationToken)
        {
            var result = 0;
            var listFavorites = await _repository.GetFavoriteCharacters();
            if(listFavorites.Count() < 5)
            {
                result = await _repository.AddFavoriteCharacter(request.FavoriteCharacter);
                return result;
            }
            return result;
        }
    }
}
