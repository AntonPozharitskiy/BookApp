using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.Entities;

namespace BLL.Finders
{
    public interface IBookFinder
    {
        Book GetById(int id);
        IEnumerable<Book> GetAll(Guid id);
        bool IsBookExists(Book book);
    }
}
