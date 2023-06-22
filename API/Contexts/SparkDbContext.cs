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
    public DbSet<Admin> Admins { get; set; }
    public DbSet<EventForm> EventForms { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Admin>().HasIndex(a => new
        {
            a.username
        }).IsUnique();

        builder.Entity<User>().HasIndex(u => new
        {
            u.Username,
            u.Email,
            u.PhoneNumber
        }).IsUnique();


        builder.Entity<Admin>()
           .HasOne(ad => ad.Account)
           .WithOne(ac => ac.Admin)
           .HasForeignKey<Admin>(ad => ad.Guid);

        builder.Entity<User>()
           .HasOne(ac => ac.Account)
           .WithOne(u => u.User)
           .HasForeignKey<User>(u => u.Guid);

        builder.Entity<Payment>()
           .HasOne(u => u.User)
           .WithOne(p => p.Payment)
           .HasForeignKey<Payment>(p => p.UserGuid);

        builder.Entity<Payment>()
           .HasOne(e => e.Event)
           .WithOne(p => p.Payment)
           .HasForeignKey<Payment>(p => p.EventGuid);

        builder.Entity<EventForm>()
           .HasOne(u => u.User)
           .WithOne(ep => ep.EventForm)
           .HasForeignKey<Payment>(ep => ep.UserGuid);

        builder.Entity<EventForm>()
           .HasOne(e => e.Event)
           .WithMany(ep => ep.EventForms)
           .HasForeignKey(ep => ep.Guid);

        builder.Entity<Event>()
           .HasOne(e => e.EventForm)
           .WithMany(ep => ep.Events)
           .HasForeignKey(ep => ep.Guid);

        builder.Entity<Event>()
           .HasOne(u => u.User)
           .WithMany(e => e.Events)
           .HasForeignKey(e => e.CreateBy);

        builder.Entity<User>()
           .HasOne(e => e.Event)
           .WithMany(u => u.Users)
           .HasForeignKey(u => u.Guid);



    }
}