using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BLL.Entities;
using BLL.Finders;
using Microsoft.EntityFrameworkCore;

namespace DAL.Finder
{
    public class BookFinder : Finder<Book>, IBookFinder
    {
        public BookFinder(DbSet<Book> entity) : base(entity)
        {
        }

        public Book GetById(int id)
        {
            return AsQueryable().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Book> GetAll(Guid id)
        {
            return AsQueryable().Where(x => x.AuthorId == id.ToString()).ToList();
        }

        public bool IsBookExists(Book book)
        {
            if (book == null) return false;
            return AsQueryable().Any(x => x.Id == book.Id);
        }
    }
}
