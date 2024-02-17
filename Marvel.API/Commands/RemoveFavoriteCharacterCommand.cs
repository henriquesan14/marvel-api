using MediatR;

namespace Marvel.API.Commands
{
    public class RemoveFavoriteCharacterCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
