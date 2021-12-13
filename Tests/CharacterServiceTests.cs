using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Controllers;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;
using dotnet_rpg.Services.CharacterService;
using Moq;
using Xunit;

namespace dotnet_rpg.Tests
{
    public class CharacterServiceTest 
    {
        private readonly Mock<ICharacterService> _sut;
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly Mock<DataContext> _dataContextMock = new Mock<DataContext>();

        public CharacterServiceTest()
        {
           _sut = new Mock<ICharacterService>();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCustomer_WhenCustomerExists()
        {
            //Arrange
            var characterId = 1;
            var characterName = "TestName";
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var characterDto = new GetCharacterDto
            {
                Id = characterId,
                Name = characterName
                
            };
            serviceResponse.Data = characterDto;
            _sut.Setup(x => x.GetCharacterById(characterId))
                .ReturnsAsync(serviceResponse);            

            //Act
            var character = await _sut.Object.GetCharacterById(characterId);

            //Assert
            Assert.Equal(characterId, character.Data.Id);
            Assert.Equal(characterName, character.Data.Name);
        }
    }
}