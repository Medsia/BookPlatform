using AutoMapper;
using BookPlatform.BLL.Intefaces;
using BookPlatform.BLL.Models;
using BookPlatform.DAL.Entities;
using BookPlatform.DAL.Intefaces;
using Microsoft.EntityFrameworkCore;

namespace BookPlatform.BLL.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<int> AddBookAsync(BookModel book)
        {
            return await _bookRepository.AddBookAsync(_mapper.Map<Book>(book));
        }

        public async Task<int> DeleteBookAsync(int id)
        {
            return await _bookRepository.DeleteBookAsync(id);
        }

        public async Task<BookModel> GetBookAsync(int id)
        {
            return _mapper.Map<BookModel>(await _bookRepository.GetBookAsync(id));
        }

        public async Task<IEnumerable<BookModel>> GetBooksAsync()
        {
            return _mapper.Map<IEnumerable<BookModel>>(await _bookRepository.GetBooksAsync());
        }

        public async Task<int> UpdateBookAsync(BookModel book)
        {
            return await _bookRepository.UpdateBookAsync(_mapper.Map<Book>(book));
        }
    }
}
