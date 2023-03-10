using BookPlatform.DAL.Entities;

namespace BookPlatform.DAL.Intefaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<Book> GetBookAsync(int id);
        Task<int> AddBookAsync(Book book);
        Task<int> UpdateBookAsync(Book book);
        Task<int> DeleteBookAsync(int id);
    }

}
