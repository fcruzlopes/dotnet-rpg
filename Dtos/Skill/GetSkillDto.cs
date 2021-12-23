using System.Collections.Generic;
using dotnet_rpg.Dtos.Character;

namespace dotnet_rpg.Dtos.Skill
{
    public class GetSkillDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }
        public List<GetCharacterDto> Characters { get; set; }
    }
}