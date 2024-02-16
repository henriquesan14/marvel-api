using Marvel.API.InputModels;
using Marvel.API.Services;
using Marvel.API.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Marvel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarvelController : ControllerBase
    {
        private readonly IMarvelApiService _service;

        public MarvelController(IMarvelApiService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> Get(string? name)
        {
            var ts = 1708119314;
            var publicKey = "244dc76eb3ac590620abe18434f15dd0";
            var privateKey = "d311d52d5152d2f3729df9f5dc1bb6894963f5a6";
            var parameters = ts +privateKey+publicKey;
            var hash = parameters.ToMD5Hash();
            var parametersApi = new RequestApiParameter {
               ApiKey = publicKey,
               Hash = hash,
               Ts = ts,
               Name = name
            };
            var result = await _service.GetCharacters(parametersApi);

            return Ok(result);
        }
    }
}
