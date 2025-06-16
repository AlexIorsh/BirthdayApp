using Microsoft.EntityFrameworkCore;
using BirthdayApp.Models;

namespace BirthdayApp.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Birthday> Birthdays { get; set; }
    }
}
