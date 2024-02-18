using Marvel.API.Entities;
using Marvel.API.ViewModels;
using MediatR;

namespace Marvel.API.Queries
{
    public class GetCharactersQuery : IRequest<ResponseAPIViewModel<Character>>
    {
        public int Limit { get; set; } = 20;
        public int Offset { get; set; }
        public string? Name { get; set; }
    }
}
