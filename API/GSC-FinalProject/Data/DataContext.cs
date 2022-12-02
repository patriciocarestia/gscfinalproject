using Microsoft.EntityFrameworkCore;
using System;

namespace GSC_FinalProject.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Entities.Person> Persons { get; set; }
        public DbSet<Entities.Loan> Loans { get; set; }
        public DbSet<Entities.Thing> Things { get; set; }
        public DbSet<Entities.Category> Categories { get; set; }
        public DbSet<Entities.User> Users { get; set; }
    }
}
