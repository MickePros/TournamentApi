using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entities;

namespace Tournament.Core.Repositories
{
    public interface ITournamentRepository
    {
        Task<IEnumerable<Tournaments>> GetAllAsync();
        Task<Tournaments> GetAsync(int id);
        Task<bool> AnyAsync(int id);
        void Add(Tournaments tournaments);
        void Update(Tournaments tournaments);
        void Remove(Tournaments tournaments);
    }
}
