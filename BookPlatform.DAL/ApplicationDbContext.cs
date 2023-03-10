using BookPlatform.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookPlatform.DAL
{
    public partial class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }

    }
}