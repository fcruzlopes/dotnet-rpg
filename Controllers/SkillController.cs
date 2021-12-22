using System.Threading.Tasks;
using dotnet_rpg.Models;
using dotnet_rpg.Services.SkillService;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController, Route("[controller]")]
    public class SkillController : ControllerBase
    {
        public ISkillService _skillService { get; set; }

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<Skill>>> GetAllSkills(int characterId)
        {
            var response = await _skillService.GetAllSkills(characterId);

            return Ok(response);
        }
    }
}