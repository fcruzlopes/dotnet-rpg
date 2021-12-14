using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Constants;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /*
            Using dependency injection. Check the ConfigureServices in Startup.cs
        */
        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
        }

        /*
            Get the id of the current user from the Claims with the HttpContextAccessor injected
        */
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character character = _mapper.Map<Character>(newCharacter);
            character.User = await _context.Users.FirstOrDefaultAsync(c => c.Id.Equals(GetUserId()));
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.Characters.Where(c => c.User.Id.Equals(GetUserId()))
                                                            .Select(c => _mapper.Map<GetCharacterDto>(c))
                                                            .ToListAsync();
            return serviceResponse;
        }

        /*
            Delete a RPG character from the user based on a id
        */
        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                Character character = await _context.Characters.FirstOrDefaultAsync(c => c.Id.Equals(id) &&
                                                                                    c.User.Id.Equals(GetUserId()));

                if (!(character is null))
                {
                    _context.Characters.Remove(character);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = await _context.Characters.Where(c => c.User.Id.Equals(GetUserId()))
                                                                    .Select(c => _mapper.Map<GetCharacterDto>(c))
                                                                    .ToListAsync();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = Messages.IdNotFound;
                }

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"{Messages.DotNetRpg}{ex.Message}";
            }
            return serviceResponse;
        }

        /*
            Get all the RPG character of the user
        */
        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters.Where(c => c.User.Id.Equals(GetUserId()))
                                                        .ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c))
                                               .ToList();
            return serviceResponse;
        }

        /*
            Get a specific RPG character based on the given id
        */
        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id.Equals(id) &&
                                                                                c.User.Id.Equals(GetUserId()));
                serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
                serviceResponse.Message = dbCharacter.ToString();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"{Messages.DotNetRpg}{ex.Message}";
            }
            return serviceResponse;
        }

        /*
            Update a specific RPG character with the new given values. Id of the RPG charcters is not
            changeable
        */
        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = await _context.Characters.FirstOrDefaultAsync(c => c.Id.Equals(updateCharacter.Id));
                if (character.User.Id.Equals(GetUserId()))
                {
                    character.Name = updateCharacter.Name;
                    character.HitPoints = updateCharacter.HitPoints;
                    character.Strength = updateCharacter.Strength;
                    character.Defense = updateCharacter.Defense;
                    character.Intelligence = updateCharacter.Intelligence;
                    character.Class = updateCharacter.Class;
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = Messages.IdNotFound;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"{Messages.DotNetRpg}{ex.Message}";
            }
            return serviceResponse;
        }
    }
}