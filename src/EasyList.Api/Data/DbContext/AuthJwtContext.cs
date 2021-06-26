using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EasyList.Api.Data
{
  public class AuthJwtContext : IdentityDbContext
  {
    public AuthJwtContext(DbContextOptions<AuthJwtContext> options) : base(options)
    {
    }

   // public DbSet<IdentityDbContext> Identity  {get; set;}

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
    }
  }
}
