using Microsoft.EntityFrameworkCore;

namespace UrlShort.Models.DB;

public class AppDbContext : DbContext
{
    public virtual DbSet<ShortUrl> Urls { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
}