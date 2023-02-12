using BlazorKiteConnect.Server.Constants;
using BlazorKiteConnect.Shared.KiteModel;
using Microsoft.EntityFrameworkCore;

namespace BlazorKiteConnect.Server.Persistence;

public class KiteAppContext : DbContext
{
    public DbSet<Instrument> Instruments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=" + Settings.PersistancePath);
    }
}