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


}