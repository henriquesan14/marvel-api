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
            var result = await _repository.RemoveFromFavorites(request.Id);
            if(result == 1)
            {
                return result;
            }
            throw new NotFoundException($"Favorite Character with id {request.Id} not exists.");
            
        }
    }
}
