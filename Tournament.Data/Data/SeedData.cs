using Bogus;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entities;

namespace Tournament.Data.Data
{
    public class SeedData
    {
        public static async Task Init(TournamentApiContext context)
        {
            var tournaments = GenerateTournaments(4);
            context.AddRange(tournaments);
            await context.SaveChangesAsync();
        }

        private static IEnumerable<Tournaments> GenerateTournaments(int nrOfTournaments)
        {
            var faker = new Faker<Tournaments>("sv").Rules((f, t) =>
            {
                t.Title = f.Name.JobTitle();
                t.StartDate = f.Date.Soon();
                t.Games = GenerateGames(f.Random.Int(min: 2, max: 5));
            });

            return faker.Generate(nrOfTournaments);
        }

        private static ICollection<Game> GenerateGames(int nrOfGames)
        {
            var faker = new Faker<Game>("sv").Rules((f, g) =>
            {
                g.Title = f.Person.FullName;
                g.Time = f.Date.Soon();
            });

            return faker.Generate(nrOfGames);
        }
    }
}
