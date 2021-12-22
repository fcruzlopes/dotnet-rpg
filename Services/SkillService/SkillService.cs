using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.SkillService
{
    public class SkillService : ISkillService
    {
        /*
            Gets all skills from a specific character matching the given Id
        */
        public Task<ServiceResponse<List<Skill>>> GetAllSkills(int characterId)
        {
            throw new System.NotImplementedException();
        }
    }
}