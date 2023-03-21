using Microsoft.AspNetCore.Identity;
using TicketingSystemIT.Entities.Models;
using TicketingSystemIT.Entities.Resources;

namespace TicketingSystemIT.Persistence;

public class Seed
{
    public static void SeedRolesSupervisor(UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        if (!roleManager.Roles.Any())
        {
            roleManager.CreateAsync(new Role { Name = Policies.Supervisor }).Wait();
            roleManager.CreateAsync(new Role { Name = Policies.ITEmployee }).Wait();
            roleManager.CreateAsync(new Role { Name = Policies.Employee }).Wait();
        }
        if (!userManager.Users.Any())
        {
            var supervisor = new User
            {
                Email = "supervisor@admin.com",
                UserName = "supervisor",
            };
            userManager.CreateAsync(supervisor, "Supervisor-12345678900").Wait();
            supervisor = userManager.FindByEmailAsync(supervisor.Email).Result;
            userManager.AddToRoleAsync(supervisor, Policies.Supervisor).Wait();
            userManager.AddToRoleAsync(supervisor, Policies.ITEmployee).Wait();
            userManager.AddToRoleAsync(supervisor, Policies.Employee).Wait();
        }
    }
}