namespace Identity.Infrastructure.EFCore;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Authority> Authorities { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<RoleAuthority> RoleAuthority { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RoleAuthority>().HasKey(ra => new { ra.RoleId, ra.AuthorityId });
        base.OnModelCreating(modelBuilder);
    }
}