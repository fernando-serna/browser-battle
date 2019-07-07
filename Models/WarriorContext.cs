using Microsoft.EntityFrameworkCore;

namespace BrowserBattle.Models
{
    public class WarriorContext : DbContext
    {
        public WarriorContext(DbContextOptions<WarriorContext> options) : base(options)
        {
        }

        public DbSet<Warrior> Warriors { get; set; }
    }
}
