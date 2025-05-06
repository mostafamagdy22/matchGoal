using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Text.Json;

namespace MatchGoalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
		private readonly IMatchRepository _matchRepostory;
		private readonly IMapper _mapper;
		public MatchesController(IMapper mapper,IMatchRepository matchRepostory)
		{
			_matchRepostory = matchRepostory;
			_mapper = mapper;
		}
		/// <summary>
		/// Get all matches from the database
		/// </summary>
		/// <returns>A <see cref="List{Match}"/></returns>
		/// <exception cref="NotFoundObjectResult"></exception>
		[HttpGet]
		public async Task<IActionResult> GetAllMatches([FromQuery]int pageSize = 10,[FromQuery]int page = 1,string filter = "")
		{
			List<Match> matches = await _matchRepostory.GetAll(pageSize,page,filter);
			if (matches == null)
			{
				return NotFound();
			}

			List<MatchDto> matchesResponse = _mapper.Map<List<MatchDto>>(matches);

			return Ok(matchesResponse);
		}
		[HttpGet("Match/{id}")]
		public async Task<IActionResult> GetMatch(int id)
		{
			if (id <= 0)
				return BadRequest("Invalid ID");

			Match match = await _matchRepostory.GetByID(id);
			if (match == null)
				return NotFound();

			MatchDto matchResponse = _mapper.Map<MatchDto>(match);

			return Ok(matchResponse);
		}
		[HttpPost("filter")]
		public async Task<IActionResult> FiltersMatch([FromBody]MatchFilterDto matchFilterDto)
		{
			List<Match> matches = await _matchRepostory.GetMatchesByFilter(matchFilterDto);
			if (matches == null)
				return NotFound();

			List<MatchDto> matchesResponse = _mapper.Map<List<MatchDto>>(matches);
			return Ok(matchesResponse);
		}
		[HttpPost("AddMatch")]
		[Authorize(Roles = "Admin",AuthenticationSchemes = "Cookies,Bearer")]
		public async Task<IActionResult> AddMatch([FromBody] AddMatchDto addMatchDto)
		{
			if (ModelState.IsValid)
			{
				Match match = _mapper.Map<Match>(addMatchDto);
				bool result = await _matchRepostory.Add(match);
				if (result)
					return Created("/m",_mapper.Map<MatchDto>(match));

				return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
			}
			return BadRequest(ModelState);
		}
		[HttpDelete("RemoveMatch/{id}")]
		[Authorize("Admin")]
		public async Task<IActionResult> RemoveMatch()
		{
			return Ok();
		}
	}
}
