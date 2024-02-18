using Marvel.API.Commands;
using Marvel.API.Exceptions;
using Marvel.API.Infra.Repositories;
using Moq;

namespace Marvel.Tests.Commands
{
    public class RemoveFavoriteCharacterCommandTests
    {
        private readonly Mock<IMarvelRepository> _mockMarvelRepository;

        public RemoveFavoriteCharacterCommandTests()
        {
            _mockMarvelRepository = new Mock<IMarvelRepository>();
        }

        [Fact]
        public async Task RemoveFavoriteCharacter_Executed_ReturnSuccess()
        {
            var command = new RemoveFavoriteCharacterCommand
            {
                Id = 1
            };
            _mockMarvelRepository.Setup(mr => mr.RemoveFromFavorites(It.IsAny<int>())).ReturnsAsync(1);
            var commandHandler = new RemoveFavoriteCharacterCommandHandler(_mockMarvelRepository.Object);
            var result = await commandHandler.Handle(command, new CancellationToken());
            Assert.NotEqual(result, 0);
        }

        [Fact]
        public async Task RemoveFavoriteCharacter_WhenFavoriteCharacterNotExist_ThrowNotFoundException()
        {
            var command = new RemoveFavoriteCharacterCommand
            {
                Id = 1
            };
            _mockMarvelRepository.Setup(mr => mr.RemoveFromFavorites(It.IsAny<int>())).ReturnsAsync(0);
            var commandHandler = new RemoveFavoriteCharacterCommandHandler(_mockMarvelRepository.Object);

            await Assert.ThrowsAsync<NotFoundException>(async () => await commandHandler.Handle(command, new CancellationToken()));
        }

    }
}
