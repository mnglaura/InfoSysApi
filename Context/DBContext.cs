using infosysapi.Models;
using Microsoft.EntityFrameworkCore;

namespace infosysapi.Context
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)                            
                : base(options)
        {
        }

        public DbSet<Student> students { get; set; } 

        public DbSet<Professor> professors { get; set; } 

        public DbSet<Cours> courses { get; set; } 

        public DbSet<StudEnrollment> studenrollments { get; set; } 

        public DbSet<ProfTeaching> profteachings { get; set; }

        public DbSet<Grade> grades { get; set; }

        public DbSet<Homework> homeworks { get; set; }

        public DbSet<User> users { get; set; }
    }
}