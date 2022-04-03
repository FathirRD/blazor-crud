using Microsoft.EntityFrameworkCore;
using bpgweb.Shared.Models;

namespace bpgweb.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>().HasData(
                new Group { ID = 1, Name = "Basic" },
                new Group { ID = 2, Name = "Staff" }
            );

            modelBuilder.Entity<User>().HasData(
                new User { 
                    ID = 1, 
                    FirstName = "Tester", 
                    LastName = "Numone", 
                    Username = "tester1", 
                    GroupID = 1
                },
                new User { 
                    ID = 2, 
                    FirstName = "Tester", 
                    LastName = "Two", 
                    Username = "tester2", 
                    GroupID = 2
                }
            );
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Group> Groups { get; set; }
    }
}
