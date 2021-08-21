using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// import the Models (representing structure of tables in database)
using SMS.Data.Models; 

namespace SMS.Data.Repositories
{
    // The Context is How EntityFramework communicates with the database
    // We define DbSet properties for each table in the database
    public class DatabaseContext :DbContext
    { 
        // create DbSets for various models
        public DbSet<User> Users { get; set; }
      
        public DbSet<Student> Students { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        
        // Configure the context to use Specified database. We are using 
        // Sqlite database as it does not require any additional installations.
        // Could use SqlServer using connection below if installed
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .LogTo(Console.WriteLine, LogLevel.Information)
	            .UseSqlite("Filename=data.db");
        }

        // Convenience method to recreate the database thus ensuring  the new database takes 
        // account of any changes to the Models or DatabaseContext
        public void Initialise()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

    }
}
