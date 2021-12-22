using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.SkillService
{
    public interface ISkillService
    {
        Task<ServiceResponse<List<Skill>>> GetAllSkills(int characterId);
    }
}