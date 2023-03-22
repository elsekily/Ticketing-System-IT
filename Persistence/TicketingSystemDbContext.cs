using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketingSystemIT.Entities.Models;

namespace TicketingSystemIT.Persistence;


public class TicketingSystemDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>,
    UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public TicketingSystemDbContext(DbContextOptions<TicketingSystemDbContext> options)
    : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserRole>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });

        builder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.Roles)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();

        builder.Entity<UserRole>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.User)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();

        builder.Entity<Ticket>()
            .HasOne(t => t.Category)
            .WithMany(c => c.Tickets);

        builder.Entity<Ticket>()
            .HasOne(t => t.UserIssued)
            .WithMany(u => u.TicketsIssued)
            .HasForeignKey(t => t.UserIssuedId);

        builder.Entity<Ticket>()
            .HasOne(t => t.AssignedEmployee)
            .WithMany(u => u.TicketsSolved)
            .HasForeignKey(t => t.AssignedEmployeeID)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);
        ;
    }
}