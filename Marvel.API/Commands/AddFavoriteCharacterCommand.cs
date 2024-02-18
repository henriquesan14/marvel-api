using Marvel.API.Entities;
using MediatR;

namespace Marvel.API.Commands
{
    public class AddFavoriteCharacterCommand : IRequest<Character>
    {
        public int Id { get; set; }
    }
}
