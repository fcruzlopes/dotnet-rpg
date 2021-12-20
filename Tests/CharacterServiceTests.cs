using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public CharacterServiceTest()
        {
           _sut = new Mock<ICharacterService>();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCharacter_WhenCharacterExists()
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

        [Fact]
        public async Task AddCharacterAsync_ShouldAddCharacter_WhenUserAddCharacter()
        {
            //Arrange
            var characterId = 1;
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var characterDto = new GetCharacterDto()
            {
                Id = characterId,
                Name = "TestName"
            };
            var newCharacterDto = new AddCharacterDto()
            {
                Name = characterDto.Name
            };
            serviceResponse.Data = new List<GetCharacterDto>();
            serviceResponse.Data.Add(characterDto);
            _sut.Setup(x => x.AddCharacter(newCharacterDto))
                .ReturnsAsync(serviceResponse);

            //Act
            var characterList = await _sut.Object.AddCharacter(newCharacterDto);
            GetCharacterDto character = characterList.Data.FirstOrDefault(c => c.Name.Equals(newCharacterDto.Name));
            
            //Assert
            Assert.True(characterList.Data.Any());
            Assert.Equal(newCharacterDto.Name, character.Name);
        }
    }
}