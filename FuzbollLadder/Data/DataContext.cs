using FuzbollLadder.Models;
using Microsoft.EntityFrameworkCore;

namespace FuzbollLadder.Data
{
    public sealed class DataContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Match> Matches { get; set; }

        public DataContext (DbContextOptions<DataContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}