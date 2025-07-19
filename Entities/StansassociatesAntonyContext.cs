using Microsoft.EntityFrameworkCore;

namespace StansAssociates_Backend.Entities;

public partial class StansassociatesAntonyContext : DbContext
{
    public StansassociatesAntonyContext(DbContextOptions<StansassociatesAntonyContext> options) : base(options)
    {

    }

    public virtual DbSet<Route> Routes { get; set; }
    public virtual DbSet<Session> Sessions { get; set; }
    public virtual DbSet<Student> Students { get; set; }
    public virtual DbSet<StudentFeesHistory> StudentFeesHistories { get; set; }
    public virtual DbSet<Studentbysession> Studentbysessions { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<UserRole> UserRoles { get; set; }
    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.SeedData();

        modelBuilder.Entity<Student>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<User>().HasQueryFilter(x => !x.IsDeleted);
    }
}
