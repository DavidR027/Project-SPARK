using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Utility;
using System.Data;

namespace API.Contexts;
public class SparkDbContext : DbContext
{
    public SparkDbContext(DbContextOptions<SparkDbContext> options) : base(options)
    {

    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountRole> AccountRoles { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>().HasIndex(u => new
        {
            u.Username,
            u.Email,
            u.PhoneNumber
        }).IsUnique();

        builder.Entity<Payment>()
           .HasOne(u => u.User)
           .WithMany(p => p.Payments)
           .HasForeignKey(p => p.UserGuid);

        builder.Entity<Payment>()
            .HasOne(e => e.Event)
            .WithMany(p => p.Payments)
            .HasForeignKey(p => p.EventGuid);

        builder.Entity<AccountRole>()
            .HasOne(a => a.Account)
            .WithMany(ar => ar.AccountRoles)
            .HasForeignKey(ar => ar.AccountGuid);

        builder.Entity<AccountRole>()
            .HasOne(r => r.Role)
            .WithMany(ar => ar.AccountRoles)
            .HasForeignKey(ar => ar.RoleGuid);

        builder.Entity<Account>()
            .HasOne(u => u.User)
            .WithOne(a => a.Account)
            .HasForeignKey<Account>(a => a.Guid);

        builder.Entity<Event>()
           .HasOne(u => u.User)
           .WithMany(e => e.Events)
           .HasForeignKey(e => e.CreateBy);

    }
}