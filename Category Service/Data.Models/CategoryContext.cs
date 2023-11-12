using Microsoft.EntityFrameworkCore;

namespace Data.Models
{
    public class CategoryContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
