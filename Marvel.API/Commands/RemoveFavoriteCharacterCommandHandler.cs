using Marvel.API.Exceptions;
using Marvel.API.Infra.Repositories;
using MediatR;

namespace Marvel.API.Commands
{
    public class RemoveFavoriteCharacterCommandHandler : IRequestHandler<RemoveFavoriteCharacterCommand, int>
    {
        private readonly IMarvelRepository _repository;

        public RemoveFavoriteCharacterCommandHandler(IMarvelRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(RemoveFavoriteCharacterCommand request, CancellationToken cancellationToken)
        {
            var character = await _repository.GetFavoriteCharacterById(request.Id);
            if(character == null)
            {
                throw new NotFoundException($"Favorite Character with id {request.Id} not exist.");
            }

            var result = await _repository.RemoveFromFavorites(character);
            return result; 
        }
    }
}
