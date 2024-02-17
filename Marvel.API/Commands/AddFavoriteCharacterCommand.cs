using Marvel.API.Models;
using MediatR;

namespace Marvel.API.Commands
{
    public class AddFavoriteCharacterCommand : IRequest<int>
    {
        public FavoriteCharacter FavoriteCharacter { get; set; }
    }
}
