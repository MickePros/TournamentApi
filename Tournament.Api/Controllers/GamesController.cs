using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tournament.Data.Data;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using AutoMapper;
using Tournament.Core.Dto;

namespace Tournament.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IUoW _uoW;
        private readonly IMapper _mapper;
        private readonly TournamentApiContext _context;

        public GamesController(TournamentApiContext context, IUoW uoW, IMapper mapper)
        {
            _context = context;
            _uoW = uoW;
            _mapper = mapper;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGame()
        {
            var tournaments = await _uoW.GameRepository.GetAllAsync();
            return Ok(tournaments);
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> GetGame(int id)
        {
            var game = await _uoW.GameRepository.GetAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            var gameDto = _mapper.Map<GameDto>(game);

            return Ok(gameDto);
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, GameDto gameDto)
        {
            if (id != gameDto.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var game = _mapper.Map<Game>(gameDto);
                _uoW.GameRepository.Update(game);

                await _uoW.CompleteAsync();
                return Ok(gameDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (GameExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(500, "A concurrency error occurred.");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the game.");
            }
        }

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GameDto>> PostGame(GameDto gameDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var game = _mapper.Map<Game>(gameDto);
                _uoW.GameRepository.Add(game);
                await _uoW.CompleteAsync();

                var newGameDto = _mapper.Map<Game>(game);

                return CreatedAtAction("GetTournaments", new { id = gameDto.Id }, newGameDto);
            }
            catch (Exception) 
            {
                return StatusCode(500, "An error occurred while saving the game.");
            }
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await _uoW.GameRepository.GetAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            try
            {
                _uoW.GameRepository.Remove(game);
                await _uoW.CompleteAsync();

                var deletedGameDto = _mapper.Map<GameDto>(game);

                return Ok(deletedGameDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while deleting the game.");
            }
        }

        private bool GameExists(int id)
        {
            return _context.Game.Any(e => e.Id == id);
        }
    }
}
