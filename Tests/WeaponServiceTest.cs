using dotnet_rpg.Services.WeaponService;
using dotnet_rpg.Dtos.Weapon;
using Moq;
using Xunit;
using dotnet_rpg.Models;
using dotnet_rpg.Dtos.Character;

namespace dotnet_rpg.Tests
{
    public class WeaponServiceTest
    {
        private readonly Mock<IWeaponService> _sut;

        public WeaponServiceTest()
        {
            _sut = new Mock<IWeaponService>();
        }

        [Fact]
        public async void AddWeaponAsync_ShouldAddWeapon_WhenCharacterDontHaveWeapon()
        {
            // Arrange
            var damage = 100;
            var name = "Magic staff";
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var addWeaponDto = new AddWeaponDto
            {
                Name = name,
                Damage = damage,
            };
            var weapon = new GetWeaponDto
            {
                Name = addWeaponDto.Name,
                Damage = addWeaponDto.Damage,
            };
            var getCharacterDto = new GetCharacterDto
            {
                Weapon = weapon,
            };
            serviceResponse.Data = getCharacterDto;
            _sut.Setup(x => x.AddWeapon(addWeaponDto))
                    .ReturnsAsync(serviceResponse);

            // Act
            var response = await _sut.Object.AddWeapon(addWeaponDto);

            // Assert
            Assert.Equal(addWeaponDto.Damage, response.Data.Weapon.Damage);
            Assert.Equal(addWeaponDto.Name, response.Data.Weapon.Name);
        }
    }
}