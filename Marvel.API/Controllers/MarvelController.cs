using Marvel.API.Commands;
using Marvel.API.Queries;
using Marvel.API.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Marvel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarvelController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MarvelController(IMediator mediator)
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

        [ProducesResponseType<ResponseAPIViewModel<Character>>(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<ActionResult> AddFavoriteCharacter([FromBody] AddFavoriteCharacterCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == 1)
            {
                return Created();
            }
            return BadRequest(new {
                Success = false,
                Message = "Already exists 5 favorites characters"
            });
        }

        [ProducesResponseType<ResponseAPIViewModel<Character>>(StatusCodes.Status204NoContent)]
        [HttpDelete]
        public async Task<ActionResult> RemoveFavoriteCharacter([FromBody] RemoveFavoriteCharacterCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

    }
}
