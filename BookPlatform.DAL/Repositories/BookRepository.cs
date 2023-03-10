using BookPlatform.DAL.Entities;
using BookPlatform.DAL.Intefaces;
using Microsoft.EntityFrameworkCore;

namespace BookPlatform.DAL.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext db;
        public BookRepository(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }
        public async Task<int> AddBookAsync(Book book)
        {
            await db.Books.AddAsync(book);
            return await db.SaveChangesAsync();
        }

        public async Task<int> DeleteBookAsync(int id)
        {
            var bookToDelete = await db.Books.FirstOrDefaultAsync(x => x.Id == id);
            db.Books.Remove(bookToDelete);
            return await db.SaveChangesAsync();
        }

        public async Task<Book> GetBookAsync(int id)
        {
            return await db.Books.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await db.Books.ToListAsync();
        }

        public async Task<int> UpdateBookAsync(Book book)
        {
            db.Entry(book).State = EntityState.Modified;
            return await db.SaveChangesAsync();
        }
    }
}
