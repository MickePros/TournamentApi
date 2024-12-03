using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using Tournament.Data.Data;

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
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tournaments>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Tournaments> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Tournaments tournaments)
        {
            throw new NotImplementedException();
        }

        public void Update(Tournaments tournaments)
        {
            throw new NotImplementedException();
        }
    }
}
