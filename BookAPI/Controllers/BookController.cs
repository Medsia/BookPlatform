using BookPlatform.BLL.Intefaces;
using BookPlatform.BLL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web.Http.Description;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {

        private readonly ILogger<BookController> _logger;
        private readonly IBookService _bookService;
        public BookController(ILogger<BookController> logger, IBookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IEnumerable<BookModel>> Get()
        {
            return await _bookService.GetBooksAsync();
        }
        [HttpGet, Route("{id}")]
        [ResponseType(typeof(BookModel))]
        public async Task<IActionResult> GetBook(int id)
        {

            BookModel book = await _bookService.GetBookAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPut, Route("{id}")]
        [ResponseType(typeof(void))]
        public async Task<IActionResult> PutBook(int id, BookModel book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != book.Id)
            {
                return BadRequest();
            }
            try
            {
                await _bookService.UpdateBookAsync(book);
            }
            catch (Exception ex)
            {

                throw;
            }
            return StatusCode((int)HttpStatusCode.NoContent);
        }

        [HttpPost]
        [ResponseType(typeof(BookModel))]
        public async Task<IActionResult> PostBook(BookModel book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _bookService.AddBookAsync(book);
            }
            catch (Exception ex)
            {

                throw;
            }
            return Ok();
        }

        [HttpDelete, Route("{id}")]
        [ResponseType(typeof(void))]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                await _bookService.DeleteBookAsync(id);
            }
            catch (Exception ex)
            {
                throw;
            }
            return Ok();
        }
    }
}
