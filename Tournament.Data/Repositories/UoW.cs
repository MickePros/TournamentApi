using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Repositories;
using Tournament.Data.Data;

namespace Tournament.Data.Repositories
{
    public class UoW : IUoW
    {
        private readonly TournamentApiContext _context;
        public UoW(TournamentApiContext context)
        {
            this._context = context;
        }

        public ITournamentRepository TournamentRepository => new TournamentRepository(_context);

        public IGameRepository GameRepository => new GameRepository(_context);

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
