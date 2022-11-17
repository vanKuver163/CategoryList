using CategoryListWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace CategoryListWeb.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
                
        }
        public DbSet<Category> Categories { get; set; }       
    }
}
