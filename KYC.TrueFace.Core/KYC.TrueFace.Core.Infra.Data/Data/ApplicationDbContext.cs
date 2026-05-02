using KYC.TrueFace.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KYC.TrueFace.Core.Infra.Data.Data;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Partner> Partners { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserAccess> UsersAccess { get; set; }
    public DbSet<UserAccessLog> UsersAccessLogs { get; set; }
    public DbSet<PartnerCredentials> PartnersCredentials { get; set; }
    public DbSet<OnboardingResult> OnboardingsResults { get; set; }
    public DbSet<Onboarding> Onboardings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.Entity<Partner>(options =>
        {
            options
                .HasKey(x => x.Code);
        });

        modelBuilder.Entity<User>(options =>
        {
            options
                .HasKey(x => x.Code);

            options
                .HasOne(x => x.Partner)
                .WithMany()
                .HasForeignKey(x => x.CodePartner)
                .IsRequired();
        });

        modelBuilder.Entity<UserAccess>(options =>
        {
            options
                .HasKey(x => x.Code);
        });

        modelBuilder.Entity<UserAccessLog>(options =>
        {
            options
                .HasKey(x => x.Code);

            options
                .HasOne(x => x.UserAccess)
                .WithMany()
                .HasForeignKey(x => x.CodeUserAccess)
                .IsRequired();
        });

        modelBuilder.Entity<PartnerCredentials>(options =>
        {
            options
                .HasKey(x => x.Code);

            options
                .HasOne(x => x.Partner)
                .WithMany()
                .HasForeignKey(x => x.CodePartner)
                .IsRequired();
        });

        modelBuilder.Entity<Onboarding>(options =>
        {
            options
                .HasKey(x => x.Code);

            options
                .HasOne(x => x.Partner)
                .WithMany()
                .HasForeignKey(x => x.CodePartner)
                .IsRequired();
        });

        modelBuilder.Entity<OnboardingResult>(options =>
        {
            options
                .HasKey(x => x.Code);

            options
                .HasOne(x => x.Onboarding)
                .WithMany()
                .HasForeignKey(x => x.CodeOnboarding)
                .IsRequired();
        });
    }
}