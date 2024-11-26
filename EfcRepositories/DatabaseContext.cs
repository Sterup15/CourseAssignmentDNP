using System.Data.Common;
using DomainEntities;
using Microsoft.EntityFrameworkCore;

namespace EfcRepositories;

public class DatabaseContext : DbContext
{
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Comment> Comments => Set<Comment>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=G:\\My Drive\\Softwareingeniør - VIA\\3. Semester\\DNP1\\CourseAssignment\\CourseAssignment\\EfcRepositories\\database.db");
    }
}