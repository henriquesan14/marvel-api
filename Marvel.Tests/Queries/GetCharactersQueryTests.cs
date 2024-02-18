using Marvel.API.Entities;
using Marvel.API.Infra.Repositories;
using Marvel.API.InputModels;
using Marvel.API.Queries;
using Marvel.API.Services;
using Marvel.API.ViewModels;
using Moq;

namespace Marvel.Tests.Queries
{
    public class GetCharactersQueryTests
    {
        private readonly Mock<IMarvelRepository> _mockMarvelRepository;
        private readonly Mock<IMarvelApiService> _mockApiService;

        public GetCharactersQueryTests()
        {
            _mockMarvelRepository = new Mock<IMarvelRepository>();
            _mockApiService = new Mock<IMarvelApiService>();
        }

        [Fact]
        public async Task GetCharacters_Executed_ReturnOrderedList()
        {
            var responseApiViewModel = new ResponseAPIViewModel<Character> {
                Code = 200,
                Data =
                new DataResult<Character>{
                    Count = 1,
                    Limit = 1,
                    Offset = 0,
                    Results = new List<Character>(){
                        new Character
                        {
                            Id = 1,
                            Name = "A",
                            Description = "Test",
                            IsFavorite = false,
                            ResourceURI = "Test",
                            Modified = "Test",
                            Thumbnail = new Thumbnail
                            {
                                Extension = "jpg",
                                Path = "Test",
                            }
                        },
                        new Character
                        {
                            Id = 2,
                            Name = "T",
                            Description = "Test",
                            IsFavorite = false,
                            ResourceURI = "Test",
                            Modified = "Test",
                            Thumbnail = new Thumbnail
                            {
                                Extension = "jpg",
                                Path = "Test",
                            }
                        },
                        new Character
                        {
                            Id = 3,
                            Name = "D",
                            Description = "Test",
                            IsFavorite = false,
                            ResourceURI = "Test",
                            Modified = "Test",
                            Thumbnail = new Thumbnail
                            {
                                Extension = "jpg",
                                Path = "Test",
                            }
                        }
                    }
                },
                Status = "OK"
            };
            var list = new List<Character>() {
                new Character
                {
                    Id = 111,
                    Name = "Z",
                    Description = "Test",
                    IsFavorite = true,
                    ResourceURI = "Test",
                    Modified = "Test",
                    Thumbnail = new Thumbnail
                    {
                        Extension = "jpg",
                        Path = "Test",
                    }
                },
                new Character
                {
                    Id = 222,
                    Name = "Bb",
                    Description = "Test",
                    IsFavorite = true,
                    ResourceURI = "Test",
                    Modified = "Test",
                    Thumbnail = new Thumbnail
                    {
                        Extension = "jpg",
                        Path = "Test",
                    }
                }
            };

            var query = new GetCharactersQuery
            {
                Limit = 20,
                Offset = 0
            };

            _mockMarvelRepository.Setup(mr => mr.GetFavoriteCharacters()).ReturnsAsync(list);
            _mockApiService.Setup(ms => ms.GetCharacters(It.IsAny<RequestApiParameter>())).ReturnsAsync(responseApiViewModel);

            var queryHandler = new GetCharactersQueryHandler(_mockApiService.Object, _mockMarvelRepository.Object);
            var result = await queryHandler.Handle(query, new CancellationToken());

            Assert.NotNull(result);
            Assert.Equal("Bb", result.Data.Results[0].Name);
            Assert.Equal("Z", result.Data.Results[1].Name);
            Assert.Equal("A", result.Data.Results[2].Name);
            Assert.Equal("D", result.Data.Results[3].Name);
            Assert.Equal("T", result.Data.Results[4].Name);
        }
    }
}
