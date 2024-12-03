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
    public class TournamentsController : ControllerBase
    {
        private readonly IUoW _uoW;
        private readonly IMapper _mapper;
        private readonly TournamentApiContext _context;

        public TournamentsController(TournamentApiContext context, IUoW uoW, IMapper mapper)
        {
            _context = context;
            _uoW = uoW;
            _mapper = mapper;
        }

        // GET: api/Tournaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDto>>> GetTournaments()
        {
            var tournaments = await _uoW.TournamentRepository.GetAllAsync();
            return Ok(tournaments);
        }

        // GET: api/Tournaments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentDto>> GetTournaments(int id)
        {
            var tournaments = await _uoW.TournamentRepository.GetAsync(id);

            if (tournaments == null)
            {
                return NotFound();
            }

            var tournamentsDto = _mapper.Map<TournamentDto>(tournaments);

            return Ok(tournamentsDto);
        }

        // PUT: api/Tournaments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournaments(int id, TournamentDto tournamentsDto)
        {
            if (id != tournamentsDto.Id)
            {
                return BadRequest();
            }

            try
            {
                var tournaments = _mapper.Map<Tournaments>(tournamentsDto);
                _uoW.TournamentRepository.Update(tournaments);

                await _uoW.CompleteAsync();
                return Ok(tournamentsDto);
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
        public async Task<ActionResult<TournamentDto>> PostTournaments(TournamentDto tournamentsDto)
        {
            var tournaments = _mapper.Map<Tournaments>(tournamentsDto);
            _uoW.TournamentRepository.Add(tournaments);
            await _uoW.CompleteAsync();

            var newTournamentsDto = _mapper.Map<Tournaments>(tournaments);

            return CreatedAtAction("GetTournaments", new { id = tournamentsDto.Id }, newTournamentsDto);
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
