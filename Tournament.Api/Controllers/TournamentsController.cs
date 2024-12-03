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

namespace Tournament.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly IUoW _uoW;
        private readonly TournamentApiContext _context;

        public TournamentsController(TournamentApiContext context, IUoW uoW)
        {
            _context = context;
            _uoW = uoW;
        }

        // GET: api/Tournaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tournaments>>> GetTournaments()
        {
            var tournaments = await _uoW.TournamentRepository.GetAllAsync();
            return Ok(tournaments);
        }

        // GET: api/Tournaments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tournaments>> GetTournaments(int id)
        {
            var tournaments = await _uoW.TournamentRepository.GetAsync(id);

            if (tournaments == null)
            {
                return NotFound();
            }

            return Ok(tournaments);
        }

        // PUT: api/Tournaments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournaments(int id, Tournaments tournaments)
        {
            if (id != tournaments.Id)
            {
                return BadRequest();
            }

            try
            {
                _uoW.TournamentRepository.Update(tournaments);

                await _uoW.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TournamentsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tournaments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tournaments>> PostTournaments(Tournaments tournaments)
        {
            _uoW.TournamentRepository.Add(tournaments);
            await _uoW.CompleteAsync();

            return CreatedAtAction("GetTournaments", new { id = tournaments.Id }, tournaments);
        }

        // DELETE: api/Tournaments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournaments(int id)
        {
            var tournaments = await _uoW.TournamentRepository.GetAsync(id);
            if (tournaments == null)
            {
                return NotFound();
            }

            _uoW.TournamentRepository.Remove(tournaments);
            await _uoW.CompleteAsync();

            return NoContent();
        }

        private bool TournamentsExists(int id)
        {
            return _context.Tournaments.Any(e => e.Id == id);
        }
    }
}
