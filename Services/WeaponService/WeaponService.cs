using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Constants;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Dtos.Weapon;
using dotnet_rpg.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public WeaponService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        /*
            Add a weapon to a RPG character
        */
        public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = await _context.Characters.FirstOrDefaultAsync(c => c.Id.Equals(newWeapon.CharacterId) &&
                                                                                         c.User.Id.Equals(GetUserId()));

                if (character is null || character.Weapon != null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"{Messages.CharacterNotFound} or {Messages.WeaponAlreadyExists}";
                    return serviceResponse;
                }
                Weapon Weapon = new Weapon
                {
                    Name = newWeapon.Name,
                    Damage = newWeapon.Damage,
                    Character = character
                };
                _context.Weapons.Add(Weapon);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"{Messages.DotNetRpg}{ex.Message}{ex.StackTrace}";
            }
            return serviceResponse;
        }
    }
}