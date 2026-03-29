using KYC.TrueFace.ApiPartner.Entities;
using Microsoft.EntityFrameworkCore;

namespace KYC.TrueFace.ApiPartner.Repositories.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) 
    : DbContext(options)
{
    public DbSet<Entities.UserAccess> UsersAccess => Set<Entities.UserAccess>();
    public DbSet<UserAccessLog> UserAccessLogs => Set<UserAccessLog>();
    public DbSet<User> Users => Set<User>();
}