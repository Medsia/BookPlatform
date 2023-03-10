using BookPlatform.BLL.Models;

namespace BookPlatform.BLL.Intefaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookModel>> GetBooksAsync();
        Task<BookModel> GetBookAsync(int id);
        Task<int> AddBookAsync(BookModel book);
        Task<int> UpdateBookAsync(BookModel book);
        Task<int> DeleteBookAsync(int id);
    }

}
