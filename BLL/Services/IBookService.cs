using System.Collections.Generic;
using BLL.Entities;

namespace BLL.Services
{
    public interface IBookService
    {
        void Create(Book book);
        void Delete(Book book);
        void Update(Book book);
        Book GetBook(int id);
        IEnumerable<Book> GetAll();
    }
}
