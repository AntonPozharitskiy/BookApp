using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
        public IEnumerable<Book> GetBooks()
        {
            return _service.GetAll();
        }
        
        [Route("Get")]
        [HttpGet("{id}")]
        public Book GetBook(int id)
        {
            var book = _service.GetBook(id);

            return book;
        }
        
        [Route("Edit")]
        [HttpPost("{id}")]
        public void UpdateBook(int id, Book book)
        {
            if (id != book.Id)
            {
                Book currentBook = _service.GetBook(id);
                _service.Update(currentBook);
            }

            _service.Update(book);
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
        
        [Route("Delete")]
        [HttpPost("{id}")]
        public void DeleteBook(int id)
        {
            var book = _service.GetBook(id);

            _service.Delete(book);
        }
    }
}