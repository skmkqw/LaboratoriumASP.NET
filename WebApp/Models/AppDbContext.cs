using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<ContactEntity> Contacts { get; set; }

    private string DbPath { get; set; }
    
    public AppDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;

        var path = Environment.GetFolderPath(folder);

        DbPath = Path.Join(path, "contacts.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var ADMIN_ID = Guid.NewGuid().ToString();
        var ADMIN_ROLE_ID = Guid.NewGuid().ToString();
        var USER_ID = Guid.NewGuid().ToString();
        var USER_ROLE_ID = Guid.NewGuid().ToString();

        modelBuilder.Entity<IdentityRole>()
            .HasData(
                new IdentityRole()
                {
                    Id = ADMIN_ROLE_ID,
                    Name = "admin",
                    NormalizedName = "admin".ToUpper(),
                    ConcurrencyStamp = ADMIN_ROLE_ID
                },
                new IdentityRole()
                {
                    Id = USER_ROLE_ID,
                    Name = "user",
                    NormalizedName = "user".ToUpper(),
                    ConcurrencyStamp = USER_ROLE_ID
                }
            );

        var admin = new IdentityUser()
        {
            Id = ADMIN_ID,
            UserName = "skmkqw",
            NormalizedUserName = "skmkqw".ToUpper(),
            Email = "tkorsakov77@gmail.com",
            NormalizedEmail = "tkorsakov77@gmail.com".ToUpper(),
            EmailConfirmed = true
        };
        
        var user = new IdentityUser()
        {
            Id = USER_ID,
            UserName = "artnov",
            NormalizedUserName = "artnov".ToUpper(),
            Email = "artnov@gmail.com",
            NormalizedEmail = "artnov@gmail.com".ToUpper(),
            EmailConfirmed = true
        };

        PasswordHasher<IdentityUser> hasher = new PasswordHasher<IdentityUser>();
        admin.PasswordHash = hasher.HashPassword(admin, "Skmkqw0401!");
        admin.PasswordHash = hasher.HashPassword(user, "112328Fil!");
        
        modelBuilder.Entity<IdentityUser>()
            .HasData(admin, user);

        modelBuilder.Entity<IdentityUserRole<string>>()
            .HasData(
                new IdentityUserRole<string>()
                {
                    RoleId = ADMIN_ROLE_ID,
                    UserId = ADMIN_ID
                },
                new IdentityUserRole<string>()
                {
                    RoleId = USER_ROLE_ID,
                    UserId = USER_ID
                },
                new IdentityUserRole<string>()
                {
                    RoleId = USER_ROLE_ID,
                    UserId = ADMIN_ID
                    
                }
            );
        
        modelBuilder.Entity<ContactEntity>()
            .HasData(
                new ContactEntity()
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "johndoe@gmail.com",
                    PhoneNumber = "123123123",
                    BirthDate = new DateOnly(2000, 10, 10),
                    Category = Category.Business,
                    CreatedAt = DateTime.Now
                },
                new ContactEntity()
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "janedoe@gmail.com",
                    PhoneNumber = "321231321",
                    BirthDate = new DateOnly(2002, 1, 23),
                    Category = Category.Family,
                    CreatedAt = DateTime.Now
                }
            );
}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data source={DbPath}");
    }
}