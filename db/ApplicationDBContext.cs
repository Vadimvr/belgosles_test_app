using Microsoft.EntityFrameworkCore;
using models;

namespace db
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        private string dbPath = @$"belgosles_test_db_sqlite.db";
        public string DbPath { get; }

        public ApplicationDBContext(string dbPath)
        {
            this.dbPath = dbPath;
            Database.EnsureCreated();
        }

        //public ApplicationDBContext()
        //{
        //    Database.EnsureCreated();
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionbuilder)
        {

            optionbuilder.UseSqlite($@"Data Source={dbPath}");
            SQLitePCL.Batteries.Init();
        }
    }
}
