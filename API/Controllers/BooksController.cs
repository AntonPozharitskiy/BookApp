using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL;
using BLL.Entities;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("Books")]
    [EnableCors("MyPolicy")]
    [Authorize]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _service;
        private readonly IUserManager _userManager;

        public BooksController(IBookService service, IUserManager userManager)
        {
            _service = service;
            _userManager = userManager;
        }
        
        [Route("GetAll")]
        [HttpGet]
        public async Task<Book[]> GetUserBooks()
        {
            var identity = (ClaimsIdentity)this.User.Identity;
            var userEmail = identity.FindFirst(JwtRegisteredClaimNames.Sub).Value;
            var user = await _userManager.GetUserByEmail(userEmail);
            IEnumerable<Book> userBookList = _service.GetAll(user.Id);
            Book[] bookArray = userBookList.Cast<Book>().ToArray();
            return bookArray;
        }
        
        [Route("Get")]
        [HttpGet("{id}")]
        public Book GetBook(int id)
        {
            var book = _service.GetBook(id);

            return book;
        }
        
        [Route("Update")]
        [HttpPost]
        public ActionResult<Book> UpdateBook(Book book)
        {
            if (book != null)
            {
                Book currentBook = _service.GetBook(book.Id);
                currentBook.Title = book.Title;
                currentBook.Content = book.Content;
                _service.Update(currentBook);
                return currentBook;
            }

            return null;
        }
        
        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> AddBook(Book book)
        {
            if (book.Content != null && book.Title != null)
            {
                var identity = (ClaimsIdentity)this.User.Identity;
                var userEmail = identity.FindFirst(JwtRegisteredClaimNames.Sub).Value;
                var user = await _userManager.GetUserByEmail(userEmail);
                book.AuthorId = user.Id.ToString();
                book.DateOfRelease = DateTime.Now;
                _service.Create(book);
                return Ok(book);
            }

            return BadRequest();
        }
        
        [Route("Delete/{id}")]
        [HttpPost]
        public int DeleteBook(int id)
        {
            var book = _service.GetBook(id);

            _service.Delete(book);

            return id;
        }
    }
}