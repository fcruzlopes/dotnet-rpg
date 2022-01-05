using System;
using System.Threading.Tasks;
using dotnet_rpg.Constants;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Fight;
using dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.FightService
{
    public class FightService : IFightService
    {
        private readonly DataContext _context;

        public FightService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request)
        {
            var serviceResponse = new ServiceResponse<AttackResultDto>();
            try
            {
                var attacker = await _context.Characters.Include(c => c.Weapon)
                                                        .FirstOrDefaultAsync(c => c.Id.Equals(request.AttackerId));
                var opponent = await _context.Characters.Include(c => c.Weapon)
                                                        .FirstOrDefaultAsync(c => c.Id.Equals(request.OpponentId));
                int damage = attacker.Weapon.Damage + (new Random().Next(attacker.Strength));
                damage -= new Random().Next(opponent.Defense);
                if(damage > 0)
                {
                    opponent.HitPoints -= damage;
                }

                if(opponent.HitPoints <= 0)
                {
                    serviceResponse.Message = $"{opponent.Name} {Messages.CharacterDefeated}";
                }

                await _context.SaveChangesAsync();
                serviceResponse.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    AttackerHp = attacker.HitPoints,
                    Opponent = opponent.Name,
                    OpponentHp = opponent.HitPoints,
                    Damage = damage
                };
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}