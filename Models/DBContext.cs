using Microsoft.EntityFrameworkCore;

namespace infosysapi.Models
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)                            
                : base(options)
        {
        }

        public DbSet<Student> students { get; set; } 

        public DbSet<Professor> professors { get; set; } 

        public DbSet<Curs> curs { get; set; } 
    }
}