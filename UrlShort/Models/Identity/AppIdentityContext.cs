using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UrlShort.Models.Identity;

public class AppIdentityContext : IdentityDbContext<IdentityUser>
{
    public AppIdentityContext(DbContextOptions<AppIdentityContext> options) : base(options)
    { }
}