using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Requests;
using AutoMapper;
using BLL;
using BLL.Entities;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("Books")]
    [EnableCors("MyPolicy")]
    [Authorize]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IBookService _service;
        private readonly IUserManager _userManager;

        public BooksController(IBookService service, IUserManager userManager, ILogger<BooksController> logger)
        {
            _logger = logger;
            _service = service;
            _userManager = userManager;
        }
        
        [Route("GetAll")]
        [HttpGet]
        public async Task<Book[]> GetUserBooks()
        {
            var user = await GetCurrentAuthor();
            _logger.LogTrace($"Trying to get {user.UserName} books...");
            IEnumerable<Book> userBookList = _service.GetAll(user.Id);
            Book[] bookArray = userBookList.ToArray();
            _logger.LogTrace($"{user.UserName} successfully got his books!");
            return bookArray;
        }

        public async Task<User> GetCurrentAuthor()
        {
            _logger.LogTrace($"Trying to get current user...");
            var identity = (ClaimsIdentity) this.User.Identity;
            var userEmail = identity.FindFirst(JwtRegisteredClaimNames.Sub).Value;
            var user = await _userManager.GetUserByEmail(userEmail);
            _logger.LogTrace($"Current user is {user.UserName}");
            return user;
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
        public async Task<ActionResult<object>> UpdateBook(RequestBookModel book)
        {
            var user = await GetCurrentAuthor();
            _logger.LogTrace($"{user.UserName} try to update book {book.Id}...");
            Book bookToUpdate = _service.GetBook(book.Id);
            Mapper.Map(book, bookToUpdate);
            _service.Update(bookToUpdate);
            _logger.LogTrace($"{user.UserName} successfully update book {book.Id}");
            return bookToUpdate;
        }
        
        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> AddBook(RequestBookModel book)
        {
            var user = await GetCurrentAuthor();
            _logger.LogTrace($"AddBook method called by {user.UserName}.");
            Book newBook = Mapper.Map<RequestBookModel, Book>(book);
            newBook.AuthorId = user.Id.ToString();
            newBook.DateOfRelease = DateTime.Now;
            _service.Create(newBook);
            _logger.LogTrace($"{user.UserName} successfully create a book!");
            return Ok(newBook);
        }
        
        [Route("Delete/{id}")]
        [HttpPost]
        public async Task<int> DeleteBook(int id)
        {
            var user = await GetCurrentAuthor();
            _logger.LogTrace($"DeleteBook method called by {user.UserName}.");
            Book bookToDelete = _service.GetBook(id);
            _service.Delete(bookToDelete);
            _logger.LogTrace($"{user.UserName} successfully deleted book {id}");
            return id;
        }
    }
}