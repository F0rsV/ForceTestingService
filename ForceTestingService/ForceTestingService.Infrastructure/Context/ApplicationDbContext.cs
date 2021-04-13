using ForceTestingService.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ForceTestingService.Infrastructure.Context
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<TestBase> TestBases { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
        public DbSet<Topic> Topics { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}