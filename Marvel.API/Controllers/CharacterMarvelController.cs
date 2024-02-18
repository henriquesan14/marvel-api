using Marvel.API.Commands;
using Marvel.API.Entities;
using Marvel.API.Queries;
using Marvel.API.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Marvel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterMarvelController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CharacterMarvelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType<ResponseAPIViewModel<Character>>(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult> GetCharacters([FromQuery] GetCharactersQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult> AddFavoriteCharacter([FromBody] AddFavoriteCharacterCommand command)
        {
            var result = await _mediator.Send(command);
            return Created(new Uri(result.ResourceURI), result);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete]
        public async Task<ActionResult> RemoveFavoriteCharacter([FromBody] RemoveFavoriteCharacterCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

    }
}
