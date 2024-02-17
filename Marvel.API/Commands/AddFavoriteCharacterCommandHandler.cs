using Marvel.API.Exceptions;
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
            var listFavorites = await _repository.GetFavoriteCharacters();
            if(listFavorites.Count() < 5)
            {
                var result =  await _repository.AddFavoriteCharacter(request.FavoriteCharacter);
                if(result == 1)
                {
                    return result;
                }
                throw new NotFoundException($"Character with id {request.FavoriteCharacter.Id} not exists.");
            }
            throw new MaximumFavoriteCharacterException("Already exists 5 favorites characters.");
        }
    }
}
