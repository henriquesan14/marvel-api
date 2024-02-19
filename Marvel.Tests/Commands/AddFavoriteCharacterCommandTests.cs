using Marvel.API.Commands;
using Marvel.API.Entities;
using Marvel.API.Exceptions;
using Marvel.API.Infra.Repositories;
using Marvel.API.InputModels;
using Marvel.API.Services;
using Marvel.API.ViewModels;
using Moq;

namespace Marvel.Tests.Commands
{
    public class AddFavoriteCharacterCommandTests
    {

        private readonly Mock<IMarvelRepository> _mockMarvelRepository;
        private readonly Mock<IMarvelApiService> _mockApiService;

        public AddFavoriteCharacterCommandTests()
        {
            _mockMarvelRepository = new Mock<IMarvelRepository>();
            _mockApiService = new Mock<IMarvelApiService>();
        }

        [Fact]
        public async Task AddFavoriteCharacter_Executed_ReturnSuccess()
        {
            var responseApiViewModel = new ResponseAPIViewModel<Character>
            {
                Code = 200,
                Data =
                new DataResult<Character>
                {
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
            var character = new Character
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
            };
            var list = new List<Character>() {
                new Character
                {
                    Id = 333,
                    Name = "Y",
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
                }
            };

            _mockMarvelRepository.Setup(mr => mr.GetFavoriteCharacters()).ReturnsAsync(list);
            _mockMarvelRepository.Setup(mr => mr.AddFavoriteCharacter(It.IsAny<Character>())).ReturnsAsync(character);
            _mockApiService.Setup(ms => ms.GetCharactersById(It.IsAny<RequestApiParameter>(), It.IsAny<int>())).ReturnsAsync(responseApiViewModel);

            var command = new AddFavoriteCharacterCommand { Id = 1 };
            var commandHandler = new AddFavoriteCharacterCommandHandler(_mockMarvelRepository.Object, _mockApiService.Object);
            var result = await commandHandler.Handle(command, new CancellationToken());

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        [Fact]
        public async Task AddFavoriteCharacter_WhenFavoriteCharactersGreaterThanFive_ThrowDoadorMaximumFavoriteCharacterException()
        {       
            var list = new List<Character>() {
                new Character
                {
                    Id = 444,
                    Name = "Y",
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
                    Id = 333,
                    Name = "Y",
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
                    Id = 777,
                    Name = "Y",
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
                    Id = 888,
                    Name = "Y",
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
            };

            _mockMarvelRepository.Setup(mr => mr.GetFavoriteCharacters()).ReturnsAsync(list);

            var command = new AddFavoriteCharacterCommand { Id = 1 };
            var commandHandler = new AddFavoriteCharacterCommandHandler(_mockMarvelRepository.Object, _mockApiService.Object);

            await Assert.ThrowsAsync<MaximumFavoriteCharacterException>(async () => await commandHandler.Handle(command, new CancellationToken()));
        }

        [Fact]
        public async Task AddFavoriteCharacter_WhenCharacterNotExistInApi_ThrowNotFoundException()
        {
            var responseApiViewModel = new ResponseAPIViewModel<Character>
            {
                Code = 404,
                Status = "NOTFOUND"
            };
            var list = new List<Character>() {
                new Character
                {
                    Id = 444,
                    Name = "Y",
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
                    Id = 333,
                    Name = "Y",
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
                    Id = 777,
                    Name = "Y",
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

            _mockMarvelRepository.Setup(mr => mr.GetFavoriteCharacters()).ReturnsAsync(list);
            _mockApiService.Setup(ms => ms.GetCharactersById(It.IsAny<RequestApiParameter>(), It.IsAny<int>())).ReturnsAsync(responseApiViewModel);

            var command = new AddFavoriteCharacterCommand { Id = 1 };
            var commandHandler = new AddFavoriteCharacterCommandHandler(_mockMarvelRepository.Object, _mockApiService.Object);

            await Assert.ThrowsAsync<NotFoundException>(async () => await commandHandler.Handle(command, new CancellationToken()));
        }

        [Fact]
        public async Task AddFavoriteCharacter_WhenFavoriteCharacterAlready_ThrowAlreadyFavoriteCharacterException()
        {
            var responseApiViewModel = new ResponseAPIViewModel<Character>
            {
                Code = 200,
                Data =
                new DataResult<Character>
                {
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
                    Id = 444,
                    Name = "Y",
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
                    Id = 333,
                    Name = "Y",
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
                    Id = 777,
                    Name = "Y",
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

            var character = new Character
            {
                Id = 555,
                Name = "Y",
                Description = "Test",
                IsFavorite = true,
                ResourceURI = "Test",
                Modified = "Test",
                Thumbnail = new Thumbnail
                {
                    Extension = "jpg",
                    Path = "Test",
                }
            };

            _mockMarvelRepository.Setup(mr => mr.GetFavoriteCharacters()).ReturnsAsync(list);
            _mockMarvelRepository.Setup(mr => mr.GetFavoriteCharacterById(It.IsAny<long>())).ReturnsAsync(character);
            _mockApiService.Setup(ms => ms.GetCharactersById(It.IsAny<RequestApiParameter>(), It.IsAny<int>())).ReturnsAsync(responseApiViewModel);

            var command = new AddFavoriteCharacterCommand { Id = 1 };
            var commandHandler = new AddFavoriteCharacterCommandHandler(_mockMarvelRepository.Object, _mockApiService.Object);

            await Assert.ThrowsAsync<AlreadyFavoriteCharacterException>(async () => await commandHandler.Handle(command, new CancellationToken()));
        }
    }
}
