using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using roulette_game_api.Model;

namespace roulette_game_api.Data
{
    public class roulette_game_apiContext : DbContext
    {
        public roulette_game_apiContext (DbContextOptions<roulette_game_apiContext> options)
            : base(options)
        {
        }

        public DbSet<roulette_game_api.Model.BetResults> BetResults { get; set; }
    }
}
