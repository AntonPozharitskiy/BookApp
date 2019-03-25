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
            Book[] bookArray = userBookList.ToArray();
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
        public ActionResult<object> UpdateBook(RequestBookModel book)
        {
            if (book != null)
            {
                Book bookToUpdate = _service.GetBook(book.Id);
                Mapper.Map(book,bookToUpdate);
              
                _service.Update(bookToUpdate);

                return bookToUpdate;
            }

            return null;
        }
        
        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> AddBook(RequestBookModel book)
        {
            if (book.Content != null && book.Title != null)
            {
                var identity = (ClaimsIdentity)this.User.Identity;
                var userEmail = identity.FindFirst(JwtRegisteredClaimNames.Sub).Value;
                var user = await _userManager.GetUserByEmail(userEmail);
                Book newBook = Mapper.Map<RequestBookModel, Book>(book);
                newBook.AuthorId = user.Id.ToString();
                newBook.DateOfRelease = DateTime.Now;
                _service.Create(newBook);
                return Ok(newBook);
            }

            return BadRequest();
        }
        
        [Route("Delete/{id}")]
        [HttpPost]
        public int DeleteBook(int id)
        {
            Book bookToDelete = _service.GetBook(id);
            _service.Delete(bookToDelete);

            return id;
        }
    }
}