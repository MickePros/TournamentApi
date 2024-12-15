using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using Tournament.Data.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Tournament.Data.Repositories
{
    internal class TournamentRepository : ITournamentRepository
    {
        private readonly TournamentApiContext _context;

        public TournamentRepository(TournamentApiContext context)
        {
            this._context = context;
        }

        public void Add(Tournaments tournaments)
        {
            _context.Tournaments.Add(tournaments);
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await _context.Tournaments.AnyAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Tournaments>> GetAllAsync()
        {
            return await _context.Tournaments.ToListAsync();
        }

        public async Task<IEnumerable<Tournaments>> GetAllAsyncIncludeGames()
        {
            return await _context.Tournaments.Include("Games").ToListAsync();
        }

        public async Task<Tournaments> GetAsync(int id)
        {
            return await _context.Tournaments.FindAsync(id);
        }

        public async Task<Tournaments> GetAsyncIncludeGames(int id)
        {
            return await _context.Tournaments.Include("Games").Where(t => t.Id == id).FirstAsync();
        }

        public void Remove(Tournaments tournaments)
        {
            _context.Tournaments.Remove(tournaments);
        }

        public void Update(Tournaments tournaments)
        {
            _context.Tournaments.Update(tournaments);
        }
    }
}
